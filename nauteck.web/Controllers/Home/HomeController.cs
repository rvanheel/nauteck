using System.Diagnostics;
using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Order;
using nauteck.web.Models;

namespace nauteck.web.Controllers;

public class HomeController(IMediator mediator) : BaseController(mediator)
{

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new Queries.FloorOrderQuery(), cancellationToken);
        return View(records);
    }

    public IActionResult Quotation() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
