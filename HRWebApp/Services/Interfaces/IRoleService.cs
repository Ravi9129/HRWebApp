using HRWebApp.DTOs;

namespace HRWebApp.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDetailDTO>> GetAllRolesAsync();
        Task<RoleDetailDTO> GetRoleByIdAsync(string id);
        Task CreateRoleAsync(CreateRoleDTO roleDTO);
        Task UpdateRoleAsync(string id, string newName);
        Task DeleteRoleAsync(string id);
        Task<int> GetUserCountForRoleAsync(string roleId);
    }
}
