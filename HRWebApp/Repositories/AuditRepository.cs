using HRWebApp.Data;
using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRWebApp.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(string userId, string action, string entityType, string entityId = null, string oldValues = null, string newValues = null, string ipAddress = null)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityId = entityId,
                OldValues = oldValues,
                NewValues = newValues,
                IPAddress = ipAddress,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(DateTime? fromDate = null, DateTime? toDate = null, string userId = null, string action = null)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(l => l.Timestamp >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(l => l.Timestamp <= toDate.Value);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(l => l.UserId == userId);

            if (!string.IsNullOrEmpty(action))
                query = query.Where(l => l.Action == action);

            return await query.OrderByDescending(l => l.Timestamp).ToListAsync();
        }
    }
}
