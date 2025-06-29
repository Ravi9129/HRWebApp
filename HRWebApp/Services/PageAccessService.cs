using AutoMapper;
using HRWebApp.DTOs;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Services
{
    public class PageAccessService : IPageAccessService
    {
        private readonly IPageAccessRepository _pageAccessRepository;
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;

        public PageAccessService(IPageAccessRepository pageAccessRepository, IMapper mapper, IAuditService auditService)
        {
            _pageAccessRepository = pageAccessRepository;
            _mapper = mapper;
            _auditService = auditService;
        }

        public async Task<IEnumerable<PageAccessDetailDTO>> GetAllPageAccessesAsync()
        {
            return await _pageAccessRepository.GetAllPageAccessesAsync();
        }

        public async Task<PageAccessDetailDTO> GetPageAccessByIdAsync(int id)
        {
            return await _pageAccessRepository.GetPageAccessByIdAsync(id);
        }

        public async Task CreatePageAccessAsync(CreatePageAccessDTO pageAccessDTO)
        {
            await _pageAccessRepository.CreatePageAccessAsync(pageAccessDTO);
            await _auditService.LogActionAsync("System", "Create", "PageAccess",
                $"New page access created for role: {pageAccessDTO.RoleId}, page: {pageAccessDTO.PageName}");
        }

        public async Task UpdatePageAccessAsync(int id, UpdatePageAccessDTO pageAccessDTO)
        {
            await _pageAccessRepository.UpdatePageAccessAsync(id, pageAccessDTO);
            await _auditService.LogActionAsync("System", "Update", "PageAccess", id.ToString());
        }

        public async Task DeletePageAccessAsync(int id)
        {
            await _pageAccessRepository.DeletePageAccessAsync(id);
            await _auditService.LogActionAsync("System", "Delete", "PageAccess", id.ToString());
        }

        public async Task<bool> HasAccessAsync(string roleId, string pageName, string requiredPermission)
        {
            return await _pageAccessRepository.HasAccessAsync(roleId, pageName, requiredPermission);
        }

        public async Task<IEnumerable<PageAccessDetailDTO>> GetPageAccessesByRoleAsync(string roleId)
        {
            return await _pageAccessRepository.GetPageAccessesByRoleAsync(roleId);
        }
    }
}
