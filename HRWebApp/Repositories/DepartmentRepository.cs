using AutoMapper;
using HRWebApp.Data;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDetailDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Employees)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DepartmentDetailDTO>>(departments);
        }

        public async Task<DepartmentDetailDTO> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);

            return _mapper.Map<DepartmentDetailDTO>(department);
        }

        public async Task CreateDepartmentAsync(CreateDepartmentDTO departmentDTO)
        {
            var department = _mapper.Map<Department>(departmentDTO);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDepartmentAsync(int id, UpdateDepartmentDTO departmentDTO)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return;

            _mapper.Map(departmentDTO, department);
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return;

            department.IsActive = false;
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DepartmentExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.Id == id);
        }

        public async Task<int> GetDepartmentCountAsync()
        {
            return await _context.Departments.CountAsync(d => d.IsActive);
        }

        public async Task<IEnumerable<DepartmentDetailDTO>> GetActiveDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Include(d => d.Manager)
                .Where(d => d.IsActive)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DepartmentDetailDTO>>(departments);
        }
    }
}
