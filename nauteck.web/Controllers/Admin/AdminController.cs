using Microsoft.AspNetCore.Mvc;

namespace nauteck.web.Controllers.Admin;

public class AdminController(IMediator mediator) : BaseController(mediator)
{
    public IActionResult Index()
    {
        return View();
    }
}
