using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Order;
using nauteck.data.Models.Order;

using static nauteck.core.Features.Order.Commands;

namespace nauteck.web.Controllers.Order;

public sealed class OrderController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Delete(string Id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new FloorOrderDeleteCommand(Id), cancellationToken);
        return Ok();
    }
    public async Task<IActionResult> Edit(string Id, CancellationToken cancellationToken)
    {
        var order = await Mediator.Send(new Queries.FloorOrderByIdQuery(Id), cancellationToken);
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> InsertOrUpdate(OrderPostModel orderPostModel,CancellationToken cancellationToken)
    {
        await Mediator.Send(new FloorOrderInsertOrUpdateCommand(orderPostModel, DateTime.Now, DisplayName, DealerId), cancellationToken);
        return Redirect("/");
    }
}
