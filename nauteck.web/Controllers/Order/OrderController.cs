using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.core.Features.Order;
using nauteck.web.Models.Order;

namespace nauteck.web.Controllers.Order;

public sealed class OrderController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Edit(string Id, CancellationToken cancellationToken)
    {
        var order = await Mediator.Send(new FloorOrderByIdQuery(Id), cancellationToken);
        return View(new FloorOrderViewModel 
        { 
            FloorOrder = order 
        });
    }
}
