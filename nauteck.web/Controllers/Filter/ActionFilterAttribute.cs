using Microsoft.AspNetCore.Mvc.Filters;

using nauteck.data.Enums;

using System.Security.Claims;

namespace nauteck.web.Controllers.Filter;

[AttributeUsage(AttributeTargets.Class)]
public class ActionFilterAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Method intentionally left empty.
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not BaseController c) return;
        // User
        c.DisplayName = c.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        c.Email = c.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        c.Id = Guid.TryParse(c.User.FindFirstValue(ClaimTypes.Sid), out var id) ? id : Guid.Empty;
        c.DealerId = c.User.FindFirstValue(ClaimTypes.PrimaryGroupSid) ?? string.Empty;
        // Roles
        c.IsEditor = c.User.IsInRole(nameof(RoleType.Editor));
        c.IsAdministrator = c.User.IsInRole(nameof(RoleType.Administrator));
        c.IsSuperUser = c.User.IsInRole(nameof(RoleType.SuperUser));
    }

}