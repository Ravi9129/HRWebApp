using HRWebApp.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleDetailDTO>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleByIdAsync(string id);
        Task CreateRoleAsync(CreateRoleDTO roleDTO);
        Task UpdateRoleAsync(string id, string newName);
        Task DeleteRoleAsync(string id);
        Task<bool> RoleExistsAsync(string id);
        Task<int> GetUserCountForRoleAsync(string roleId);
    }
}
