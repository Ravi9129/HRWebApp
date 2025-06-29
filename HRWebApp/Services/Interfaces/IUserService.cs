using HRWebApp.DTOs;
using HRWebApp.Models;

namespace HRWebApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByUsernameAsync(string username);
        Task<bool> CheckPasswordAsync(string userId, string password);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task<ProfileDTO> GetUserProfileAsync(string userId);
        Task UpdateUserProfileAsync(string userId, ProfileDTO profileDTO);
        Task ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<int> GetTotalUserCountAsync();
        Task<int> GetActiveUserCountAsync();
        Task<int> GetInactiveUserCountAsync();
    }
}
