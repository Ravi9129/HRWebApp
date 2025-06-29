using HRWebApp.DTOs;

namespace HRWebApp.Services.Interfaces
{
    public interface IPageAccessService
    {
        Task<IEnumerable<PageAccessDetailDTO>> GetAllPageAccessesAsync();
        Task<PageAccessDetailDTO> GetPageAccessByIdAsync(int id);
        Task CreatePageAccessAsync(CreatePageAccessDTO pageAccessDTO);
        Task UpdatePageAccessAsync(int id, UpdatePageAccessDTO pageAccessDTO);
        Task DeletePageAccessAsync(int id);
        Task<bool> HasAccessAsync(string roleId, string pageName, string requiredPermission);
        Task<IEnumerable<PageAccessDetailDTO>> GetPageAccessesByRoleAsync(string roleId);
    }
}
