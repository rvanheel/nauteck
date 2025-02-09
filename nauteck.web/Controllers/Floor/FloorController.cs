using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Floor;
using nauteck.core.Features.Floor.Color;

namespace nauteck.web.Controllers.Floor;

public sealed class FloorController(IMediator mediator) : BaseController(mediator)
{
    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> ExclusiveColor(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorColorExclusiveQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> StandardColor(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorColorQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> Logo(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorLogoQuery(), cancellationToken);
        return View(records);
    }
}

