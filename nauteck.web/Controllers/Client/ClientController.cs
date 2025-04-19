using Microsoft.AspNetCore.Mvc;

using nauteck.core.Abstraction;
using nauteck.core.Features.Client;

namespace nauteck.web.Controllers.Client;

public sealed class ClientController(IMediator mediator, IHelper helper) : BaseController(mediator)
{
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.ClientDeleteCommand(id), cancellationToken);
        return RedirectToAction(nameof(Index), "Home");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(new Queries.ClientByIdQuery(id), cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdate(data.Models.Client.ClientPostModel clientPostModel)
    {
        await Mediator.Send(new Commands.ClientUpdateCommand(clientPostModel, DisplayName, helper.AtCurrentTimeZone ));
        return RedirectToAction(nameof(Index), "Home");
    }
}
