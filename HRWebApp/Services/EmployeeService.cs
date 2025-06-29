using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IAuditService auditService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<IEnumerable<EmployeeDetailDTO>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<EmployeeDetailDTO> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<EmployeeDetailDTO> GetEmployeeByUserIdAsync(string userId)
        {
            return await _employeeRepository.GetEmployeeByUserIdAsync(userId);
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDTO employeeDTO)
        {
            await _employeeRepository.CreateEmployeeAsync(employeeDTO);
            await _auditService.LogActionAsync("System", "Create", "Employee",
                $"New employee created: {employeeDTO.FirstName} {employeeDTO.LastName}");
        }

        public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employeeDTO)
        {
            await _employeeRepository.UpdateEmployeeAsync(id, employeeDTO);
            await _auditService.LogActionAsync("System", "Update", "Employee", id.ToString());
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            await _auditService.LogActionAsync("System", "Delete", "Employee", id.ToString());
        }

        public async Task<int> GetActiveEmployeeCountAsync()
        {
            return await _employeeRepository.GetActiveEmployeeCountAsync();
        }

        public async Task<int> GetInactiveEmployeeCountAsync()
        {
            return await _employeeRepository.GetInactiveEmployeeCountAsync();
        }

        public async Task<IEnumerable<EmployeeDetailDTO>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            return await _employeeRepository.GetEmployeesByDepartmentAsync(departmentId);
        }

        public async Task<EmployeeShiftDTO> GetEmployeeShiftAsync(int employeeId)
        {
            var shift = await _employeeRepository.GetEmployeeShiftAsync(employeeId);
            return _mapper.Map<EmployeeShiftDTO>(shift);
        }

        public async Task UpdateEmployeeShiftAsync(int employeeId, EmployeeShiftDTO shiftDTO)
        {
            await _employeeRepository.UpdateEmployeeShiftAsync(employeeId, shiftDTO);
            await _auditService.LogActionAsync("System", "Update", "EmployeeShift", employeeId.ToString());
        }

        public async Task<EmployeeBankDetailDTO> GetEmployeeBankDetailAsync(int employeeId)
        {
            var bankDetail = await _employeeRepository.GetEmployeeBankDetailAsync(employeeId);
            return _mapper.Map<EmployeeBankDetailDTO>(bankDetail);
        }

        public async Task UpdateEmployeeBankDetailAsync(int employeeId, EmployeeBankDetailDTO bankDetailDTO)
        {
            await _employeeRepository.UpdateEmployeeBankDetailAsync(employeeId, bankDetailDTO);
            await _auditService.LogActionAsync("System", "Update", "EmployeeBankDetail", employeeId.ToString());
        }

        public async Task<IEnumerable<EmployeeBenefitDTO>> GetEmployeeBenefitsAsync(int employeeId)
        {
            var benefits = await _employeeRepository.GetEmployeeBenefitsAsync(employeeId);
            return _mapper.Map<IEnumerable<EmployeeBenefitDTO>>(benefits);
        }

        public async Task AddEmployeeBenefitAsync(int employeeId, EmployeeBenefitDTO benefitDTO)
        {
            await _employeeRepository.AddEmployeeBenefitAsync(employeeId, benefitDTO);
            await _auditService.LogActionAsync("System", "Add", "EmployeeBenefit", employeeId.ToString());
        }

        public async Task RemoveEmployeeBenefitAsync(int benefitId)
        {
            await _employeeRepository.RemoveEmployeeBenefitAsync(benefitId);
            await _auditService.LogActionAsync("System", "Remove", "EmployeeBenefit", benefitId.ToString());
        }
    }
}
