using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using nauteck.core.Features.Client;

namespace nauteck.web.Controllers.Home;

public class HomeController(IMediator mediator) : BaseController(mediator)
{

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new Queries.ClientQuery(), cancellationToken);
        return View(records);
    }

    public IActionResult Quotation() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
