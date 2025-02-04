using System.Diagnostics;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using nauteck.web.Models;

namespace nauteck.web.Controllers;

public class HomeController(IMediator mediator) : BaseController(mediator)
{

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
