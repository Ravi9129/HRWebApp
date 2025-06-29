// Services/UserService.cs
using System.Security.Claims;
using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ProfileDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<ProfileDTO>(user);
        }

        public async Task UpdateUserProfileAsync(string userId, ProfileDTO profileDTO)
        {
            var user = await _userManager.FindByIdAsync(userId);
            _mapper.Map(profileDTO, user);
            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
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
    }
}