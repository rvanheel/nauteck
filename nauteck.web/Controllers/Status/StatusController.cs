using MediatR;

using Microsoft.AspNetCore.Mvc;

using static nauteck.core.Features.Status.Queries;

namespace nauteck.web.Controllers.Status;

public sealed class StatusController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
    {
        var status = await Mediator.Send(new StatusQuery(), cancellationToken);
        return View(status);
    }
}
