using System.Security.Claims;
using HRWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRWebApp.Filters
{
    public class AuditActionFilter : IActionFilter
    {
        private readonly IAuditService _auditService;

        public AuditActionFilter(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Not used
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var actionName = context.ActionDescriptor.DisplayName;
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            _auditService.LogActionAsync(userId, "Access", actionName, ipAddress: ipAddress);
        }
    }
}
