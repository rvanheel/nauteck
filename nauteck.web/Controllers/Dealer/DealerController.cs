using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Dealer;

namespace nauteck.web.Controllers.Dealer;

public sealed class DealerController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new DealerQuery(), cancellationToken);
        return View(records);
    }
}
