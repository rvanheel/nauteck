using System.Threading;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Floor;
using nauteck.core.Features.Floor.Color;

namespace nauteck.web.Controllers.Floor;

public sealed class FloorController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Construction(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorConstructionQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> Design(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorDesignQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> ExclusiveColor(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorColorExclusiveQuery(), cancellationToken);
        return View(records);
    }    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> Logo(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorLogoQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> Measurement(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorMeasurementQuery(), cancellationToken);
        return View(records);
    }
    public async Task<IActionResult> StandardColor(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new FloorColorQuery(), cancellationToken);
        return View(records);
    }
}

