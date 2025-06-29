using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IAuditService auditService)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<IEnumerable<DepartmentDetailDTO>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllDepartmentsAsync();
        }

        public async Task<DepartmentDetailDTO> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetDepartmentByIdAsync(id);
        }

        public async Task CreateDepartmentAsync(CreateDepartmentDTO departmentDTO)
        {
            await _departmentRepository.CreateDepartmentAsync(departmentDTO);
            await _auditService.LogActionAsync("System", "Create", "Department",
                $"New department created: {departmentDTO.Name}");
        }

        public async Task UpdateDepartmentAsync(int id, UpdateDepartmentDTO departmentDTO)
        {
            await _departmentRepository.UpdateDepartmentAsync(id, departmentDTO);
            await _auditService.LogActionAsync("System", "Update", "Department", id.ToString());
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
            await _auditService.LogActionAsync("System", "Delete", "Department", id.ToString());
        }

        public async Task<int> GetDepartmentCountAsync()
        {
            return await _departmentRepository.GetDepartmentCountAsync();
        }
    }
}
