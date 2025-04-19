using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using nauteck.web.Controllers.Filter;


namespace nauteck.web.Controllers;

[Controller]
[Authorize]
[ActionFilter]
public abstract class BaseController(IMediator mediator) : Controller
{
    protected readonly IMediator Mediator = mediator;

    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";
    public string DealerId { get; set; } = "";
    public Guid Id { get; set; }

    public bool IsEditor { get; set; }
    public bool IsAdministrator { get; set; }
    public bool IsSuperUser { get; set; }
}
