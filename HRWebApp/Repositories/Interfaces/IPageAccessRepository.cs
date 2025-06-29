using HRWebApp.DTOs;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IPageAccessRepository
    {
        Task<IEnumerable<PageAccessDetailDTO>> GetAllPageAccessesAsync();
        Task<PageAccessDetailDTO> GetPageAccessByIdAsync(int id);
        Task CreatePageAccessAsync(CreatePageAccessDTO pageAccessDTO);
        Task UpdatePageAccessAsync(int id, UpdatePageAccessDTO pageAccessDTO);
        Task DeletePageAccessAsync(int id);
        Task<bool> PageAccessExistsAsync(int id);
        Task<bool> HasAccessAsync(string roleId, string pageName, string requiredPermission);
        Task<IEnumerable<PageAccessDetailDTO>> GetPageAccessesByRoleAsync(string roleId);
    }
}
