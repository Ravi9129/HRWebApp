using HRWebApp.Models;

namespace HRWebApp.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task LogActionAsync(string userId, string action, string entityType, string entityId = null, string oldValues = null, string newValues = null, string ipAddress = null);
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync(DateTime? fromDate = null, DateTime? toDate = null, string userId = null, string action = null);
    }
}
