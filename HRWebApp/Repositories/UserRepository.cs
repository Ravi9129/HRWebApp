using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ProfileDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<ProfileDTO>(user);
        }

        public async Task UpdateUserProfileAsync(string userId, ProfileDTO profileDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            _mapper.Map(profileDTO, user);
            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _userManager.Users.CountAsync();
        }

        public async Task<int> GetActiveUserCountAsync()
        {
            return await _userManager.Users.CountAsync(u => u.IsActive);
        }

        public async Task<int> GetInactiveUserCountAsync()
        {
            return await _userManager.Users.CountAsync(u => !u.IsActive);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }
    }
}
