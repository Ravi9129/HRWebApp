using HRWebApp.DTOs;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDetailDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDetailDTO> GetDepartmentByIdAsync(int id);
        Task CreateDepartmentAsync(CreateDepartmentDTO departmentDTO);
        Task UpdateDepartmentAsync(int id, UpdateDepartmentDTO departmentDTO);
        Task DeleteDepartmentAsync(int id);
        Task<bool> DepartmentExistsAsync(int id);
        Task<int> GetDepartmentCountAsync();
        Task<IEnumerable<DepartmentDetailDTO>> GetActiveDepartmentsAsync();
    }
}
