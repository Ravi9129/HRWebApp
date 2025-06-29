using AutoMapper;
using HRWebApp.Data;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDetailDTO>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Where(e => e.IsActive)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeDetailDTO>>(employees);
        }

        public async Task<EmployeeDetailDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);

            return _mapper.Map<EmployeeDetailDTO>(employee);
        }

        public async Task<EmployeeDetailDTO> GetEmployeeByUserIdAsync(string userId)
        {
            var employee = await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.UserId == userId);

            return _mapper.Map<EmployeeDetailDTO>(employee);
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDTO)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return;

            _mapper.Map(employeeDTO, employee);
            employee.LastModifiedDate = DateTime.UtcNow;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return;

            employee.IsActive = false;
            employee.LeavingDate = DateTime.UtcNow;
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmployeeExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task<int> GetActiveEmployeeCountAsync()
        {
            return await _context.Employees.CountAsync(e => e.IsActive);
        }

        public async Task<int> GetInactiveEmployeeCountAsync()
        {
            return await _context.Employees.CountAsync(e => !e.IsActive);
        }

        public async Task<IEnumerable<EmployeeDetailDTO>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            var employees = await _context.Employees
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Role)
                .Where(e => e.DepartmentId == departmentId && e.IsActive)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EmployeeDetailDTO>>(employees);
        }

        public async Task<EmployeeShift> GetEmployeeShiftAsync(int employeeId)
        {
            return await _context.EmployeeShifts
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId);
        }

        public async Task UpdateEmployeeShiftAsync(int employeeId, EmployeeShiftDTO shiftDTO)
        {
            var shift = await _context.EmployeeShifts
                .FirstOrDefaultAsync(s => s.EmployeeId == employeeId);

            if (shift == null)
            {
                shift = new EmployeeShift { EmployeeId = employeeId };
                _mapper.Map(shiftDTO, shift);
                _context.EmployeeShifts.Add(shift);
            }
            else
            {
                _mapper.Map(shiftDTO, shift);
                _context.EmployeeShifts.Update(shift);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeBankDetail> GetEmployeeBankDetailAsync(int employeeId)
        {
            return await _context.EmployeeBankDetails
                .FirstOrDefaultAsync(b => b.EmployeeId == employeeId);
        }

        public async Task UpdateEmployeeBankDetailAsync(int employeeId, EmployeeBankDetailDTO bankDetailDTO)
        {
            var bankDetail = await _context.EmployeeBankDetails
                .FirstOrDefaultAsync(b => b.EmployeeId == employeeId);

            if (bankDetail == null)
            {
                bankDetail = new EmployeeBankDetail { EmployeeId = employeeId };
                _mapper.Map(bankDetailDTO, bankDetail);
                _context.EmployeeBankDetails.Add(bankDetail);
            }
            else
            {
                _mapper.Map(bankDetailDTO, bankDetail);
                _context.EmployeeBankDetails.Update(bankDetail);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeBenefit>> GetEmployeeBenefitsAsync(int employeeId)
        {
            return await _context.EmployeeBenefits
                .Where(b => b.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task AddEmployeeBenefitAsync(int employeeId, EmployeeBenefitDTO benefitDTO)
        {
            var benefit = _mapper.Map<EmployeeBenefit>(benefitDTO);
            benefit.EmployeeId = employeeId;
            _context.EmployeeBenefits.Add(benefit);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveEmployeeBenefitAsync(int benefitId)
        {
            var benefit = await _context.EmployeeBenefits.FindAsync(benefitId);
            if (benefit == null) return;

            _context.EmployeeBenefits.Remove(benefit);
            await _context.SaveChangesAsync();
        }
    }
}
