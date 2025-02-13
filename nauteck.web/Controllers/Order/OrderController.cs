using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Order;
using nauteck.data.Models.Order;

namespace nauteck.web.Controllers.Order;

public sealed class OrderController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Edit(string Id, CancellationToken cancellationToken)
    {
        var order = await Mediator.Send(new Queries.FloorOrderByIdQuery(Id), cancellationToken);
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InsertOrUpdate(OrderPostModel orderPostModel,CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.FloorOrderInsertOrUpdateCommand(orderPostModel), cancellationToken);
        return Redirect("/");
    }
}
