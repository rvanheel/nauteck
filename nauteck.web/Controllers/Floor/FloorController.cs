using Microsoft.AspNetCore.Mvc;

using static nauteck.core.Features.Queries.Floor;

namespace nauteck.web.Controllers.Floor;

public sealed class FloorController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> ExclusiveColor(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorColorExclusiveQuery(), cancellationToken);
        return View(records);
    }    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> StandardColor(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorColorQuery(), cancellationToken);
        return View(records);
    }
}

