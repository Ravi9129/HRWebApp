using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public RoleService(IRoleRepository roleRepository, IMapper mapper, IAuditService auditService)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<IEnumerable<RoleDetailDTO>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllRolesAsync();
        }

        public async Task<RoleDetailDTO> GetRoleByIdAsync(string id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            return _mapper.Map<RoleDetailDTO>(role);
        }

        public async Task CreateRoleAsync(CreateRoleDTO roleDTO)
        {
            await _roleRepository.CreateRoleAsync(roleDTO);
            await _auditService.LogActionAsync("System", "Create", "Role",
                $"New role created: {roleDTO.Name}");
        }

        public async Task UpdateRoleAsync(string id, string newName)
        {
            await _roleRepository.UpdateRoleAsync(id, newName);
            await _auditService.LogActionAsync("System", "Update", "Role", id);
        }

        public async Task DeleteRoleAsync(string id)
        {
            await _roleRepository.DeleteRoleAsync(id);
            await _auditService.LogActionAsync("System", "Delete", "Role", id);
        }

        public async Task<int> GetUserCountForRoleAsync(string roleId)
        {
            return await _roleRepository.GetUserCountForRoleAsync(roleId);
        }
    }
}
