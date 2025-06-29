using HRWebApp.DTOs;

namespace HRWebApp.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDetailDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDetailDTO> GetDepartmentByIdAsync(int id);
        Task CreateDepartmentAsync(CreateDepartmentDTO departmentDTO);
        Task UpdateDepartmentAsync(int id, UpdateDepartmentDTO departmentDTO);
        Task DeleteDepartmentAsync(int id);
        Task<int> GetDepartmentCountAsync();
    }
}
