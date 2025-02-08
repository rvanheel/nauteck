using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Floor.Color;
using nauteck.data.Entities.Floor.Color;

namespace nauteck.web.Controllers.Floor;

public sealed class FloorController(IMediator mediator) : BaseController(mediator)
{
    public IActionResult Index()
    {
        return View();
    }
    #region Exclusive Colors
    public async Task<IActionResult> ExclusiveColor(CancellationToken cancellationToken)
    {
        var standardColors = await Mediator.Send(new FloorColorExclusiveQuery(), cancellationToken);
        return View(standardColors);
    }
    #endregion
    #region Standard Colors
    public async Task<IActionResult> StandardColor(CancellationToken cancellationToken)
    {
        var standardColors = await Mediator.Send(new FloorColorQuery(), cancellationToken);
        return View(standardColors);
    }
    #endregion
}

