using HRWebApp.Models;
using HRWebApp.Repositories.Interfaces;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task LogActionAsync(string userId, string action, string entityType, string entityId = null, string oldValues = null, string newValues = null, string ipAddress = null)
        {
            await _auditRepository.LogActionAsync(userId, action, entityType, entityId, oldValues, newValues, ipAddress);
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(DateTime? fromDate = null, DateTime? toDate = null, string userId = null, string action = null)
        {
            return await _auditRepository.GetAuditLogsAsync(fromDate, toDate, userId, action);
        }
    }
}
