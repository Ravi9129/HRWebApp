using System.Security.Claims;
using HRWebApp.Services.Interfaces;

namespace HRWebApp.Middleware
{
    public class PageAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public PageAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IPageAccessService pageAccessService)
        {
            var path = context.Request.Path.Value;
            var user = context.User;

            if (user.Identity.IsAuthenticated && !path.StartsWith("/Account") && !path.StartsWith("/Home"))
            {
                var roleId = user.FindFirst(ClaimTypes.Role)?.Value;
                if (roleId != null)
                {
                    var hasAccess = await pageAccessService.HasAccessAsync(roleId, path, "View");
                    if (!hasAccess)
                    {
                        context.Response.Redirect("/Account/AccessDenied");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
