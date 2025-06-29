using HRWebApp.DTOs;
using HRWebApp.Models;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ProfileDTO> GetUserProfileAsync(string userId);
        Task UpdateUserProfileAsync(string userId, ProfileDTO profileDTO);
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(string userId, string password);
        Task<int> GetTotalUserCountAsync();
        Task<int> GetActiveUserCountAsync();
        Task<int> GetInactiveUserCountAsync();
        Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName);
    }
}
