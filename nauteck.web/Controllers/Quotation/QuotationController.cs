using MediatR;

using Microsoft.AspNetCore.Mvc;

using static nauteck.core.Features.Queries.Quotation;

namespace nauteck.web.Controllers.Quotation;

public sealed class QuotationController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new QuotationQuery(), cancellationToken);
        return View(records);
    }
}
