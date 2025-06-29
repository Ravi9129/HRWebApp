using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDetailDTO>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var roleDetails = new List<RoleDetailDTO>();

            foreach (var role in roles)
            {
                var userCount = await _userManager.GetUsersInRoleAsync(role.Name);
                roleDetails.Add(new RoleDetailDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                    UserCount = userCount.Count
                });
            }

            return roleDetails;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task CreateRoleAsync(CreateRoleDTO roleDTO)
        {
            var role = new IdentityRole(roleDTO.Name);
            await _roleManager.CreateAsync(role);
        }

        public async Task UpdateRoleAsync(string id, string newName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return;

            role.Name = newName;
            await _roleManager.UpdateAsync(role);
        }

        public async Task DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return;

            await _roleManager.DeleteAsync(role);
        }

        public async Task<bool> RoleExistsAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return role != null;
        }

        public async Task<int> GetUserCountForRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return 0;

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            return users.Count;
        }
    }
}
