using AutoMapper;
using HRWebApp.Data;
using HRWebApp.DTOs;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class PageAccessRepository : IPageAccessRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public PageAccessRepository(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PageAccessDetailDTO>> GetAllPageAccessesAsync()
        {
            var pageAccesses = await _context.PageAccesses
                .Include(pa => pa.Role)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PageAccessDetailDTO>>(pageAccesses);
        }

        public async Task<PageAccessDetailDTO> GetPageAccessByIdAsync(int id)
        {
            var pageAccess = await _context.PageAccesses
                .Include(pa => pa.Role)
                .FirstOrDefaultAsync(pa => pa.Id == id);

            return _mapper.Map<PageAccessDetailDTO>(pageAccess);
        }

        public async Task CreatePageAccessAsync(CreatePageAccessDTO pageAccessDTO)
        {
            var pageAccess = _mapper.Map<PageAccess>(pageAccessDTO);
            _context.PageAccesses.Add(pageAccess);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePageAccessAsync(int id, UpdatePageAccessDTO pageAccessDTO)
        {
            var pageAccess = await _context.PageAccesses.FindAsync(id);
            if (pageAccess == null) return;

            _mapper.Map(pageAccessDTO, pageAccess);
            pageAccess.LastModifiedDate = DateTime.UtcNow;
            _context.PageAccesses.Update(pageAccess);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePageAccessAsync(int id)
        {
            var pageAccess = await _context.PageAccesses.FindAsync(id);
            if (pageAccess == null) return;

            _context.PageAccesses.Remove(pageAccess);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> PageAccessExistsAsync(int id)
        {
            return await _context.PageAccesses.AnyAsync(pa => pa.Id == id);
        }

        public async Task<bool> HasAccessAsync(string roleId, string pageName, string requiredPermission)
        {
            var pageAccess = await _context.PageAccesses
                .FirstOrDefaultAsync(pa => pa.RoleId == roleId && pa.PageName == pageName);

            if (pageAccess == null) return false;

            return requiredPermission switch
            {
                "View" => pageAccess.CanView,
                "Create" => pageAccess.CanCreate,
                "Edit" => pageAccess.CanEdit,
                "Delete" => pageAccess.CanDelete,
                _ => false
            };
        }

        public async Task<IEnumerable<PageAccessDetailDTO>> GetPageAccessesByRoleAsync(string roleId)
        {
            var pageAccesses = await _context.PageAccesses
                .Include(pa => pa.Role)
                .Where(pa => pa.RoleId == roleId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PageAccessDetailDTO>>(pageAccesses);
        }
    }
}
