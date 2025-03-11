using MediatR;

using Microsoft.AspNetCore.Mvc;

using static nauteck.core.Features.Status.Commands;
using static nauteck.core.Features.Status.Queries;

namespace nauteck.web.Controllers.Status;

public sealed class StatusController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> Edit (Guid id, CancellationToken cancellationToken)
    {
        var Status = await Mediator.Send(new StatusByIdQuery(id), cancellationToken);   
        return View(Status);
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var status = await Mediator.Send(new StatusQuery(), cancellationToken);
        return View(status);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdate(data.Entities.Status.Status status, CancellationToken cancellationToken)
    {
        await Mediator.Send(new SaveOrUpdateStatusCommand(status, DisplayName), cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
