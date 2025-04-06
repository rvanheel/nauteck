using MediatR;

using Microsoft.AspNetCore.Mvc;

using nauteck.data.Dto.Quotation;

using static nauteck.core.Features.Commands.Quotation;
using static nauteck.core.Features.Queries.Quotation;

namespace nauteck.web.Controllers.Quotation;

public sealed class QuotationController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var record = await Mediator.Send(new QuotationDeleteCommand(id), cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> DeleteQuotationLine(Guid id, Guid quotationId, CancellationToken cancellationToken)
    {
        var record = await Mediator.Send(new QuotationLineDeleteCommand(id, quotationId), cancellationToken);
        return RedirectToAction(nameof(Edit), new { id = quotationId });
    }
    public async Task<IActionResult> Edit(Guid id, Guid clientId, CancellationToken cancellationToken)
    {
        var record = id.Equals(Guid.Empty) ?
            new QuotationDto { ClientId = clientId, Date = DateTime.Now, Status = core.Implementation.Constants.QuotationStats.CONCEPT }
            : await Mediator.Send(new QuotationByIdQuery(id), cancellationToken);
        return View(record);
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new QuotationQuery(), cancellationToken);
        return View(records);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdate(QuotationDto quotation, CancellationToken cancellationToken)
    {
        var isNew = quotation.Id.Equals(Guid.Empty);
        var id = await Mediator.Send(new SaveOrUpdateQuotationCommand(quotation), cancellationToken);
        return isNew ?
            RedirectToAction(nameof(Edit), new { id })
            : RedirectToAction(nameof(Index));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdateLine(QuotationLineDto quotationLine, CancellationToken cancellationToken)
    {
        await Mediator.Send(new QuotationLineSaveOrUpdateCommand(quotationLine), cancellationToken);
        return RedirectToAction(nameof(Edit), new { id = quotationLine.QuotationId });
    }
}
