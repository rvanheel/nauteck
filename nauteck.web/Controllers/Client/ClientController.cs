using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Abstraction;
using nauteck.core.Features.Client;

namespace nauteck.web.Controllers.Client;

public sealed class ClientController(IMediator mediator, IHelper helper) : BaseController(mediator)
{
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid Id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.ClientDeleteCommand(Id), cancellationToken);
        return RedirectToAction(nameof(Index), "Home");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(Guid Id, CancellationToken cancellationToken)
    {
        var model = await Mediator.Send(new Queries.ClientByIdQuery(Id), cancellationToken);
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
