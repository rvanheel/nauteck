using Microsoft.AspNetCore.Mvc;
using nauteck.core.Features;
using nauteck.data.Dto.Invoice;

namespace nauteck.web.Controllers.Invoice;

public class InvoiceController(IMediator mediator) : BaseController(mediator)
{
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var record = await Mediator.Send(new Commands.Invoice.InvoiceDeleteCommand(id), cancellationToken);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> DeleteInvoiceLine(Guid id, Guid invoiceId, CancellationToken cancellationToken)
    {
        var record = await Mediator.Send(new Commands.Invoice.InvoiceLineDeleteCommand(id, invoiceId), cancellationToken);
        return RedirectToAction(nameof(Edit), new { id = invoiceId });
    }
    public async Task<IActionResult> Edit(Guid id, Guid clientId, CancellationToken cancellationToken)
    {
        var record = id.Equals(Guid.Empty) ?
            new InvoiceDto { ClientId = clientId, Date = DateTime.Now, Status = core.Implementation.Constants.Status.Proforma }
            : await Mediator.Send(new Queries.Invoice.InvoiceByIdQuery(id), cancellationToken);
        return View(record);
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var records = await Mediator.Send(new Queries.Invoice.InvoiceQuery(), cancellationToken);
        return View(records);
    }
    [HttpGet]
    public async Task<IActionResult> Pdf(Guid id, CancellationToken cancellationToken)
    {
        var (binaryDate, fileName) = await Mediator.Send(new Queries.Invoice.InvoicePdfQuery(id), cancellationToken);
        return File(binaryDate.ToArray(), "application/pdf", fileName);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdate(InvoiceDto invoice, CancellationToken cancellationToken)
    {
        var isNew = invoice.Id.Equals(Guid.Empty);
        var id = await Mediator.Send(new Commands.Invoice.SaveOrUpdateInvoiceCommand(invoice), cancellationToken);
        return isNew ?
            RedirectToAction(nameof(Edit), new { id })
            : RedirectToAction(nameof(Index));

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SaveOrUpdateLine(InvoiceLineDto invoiceLine, CancellationToken cancellationToken)
    {
        await Mediator.Send(new Commands.Invoice.InvoiceLineSaveOrUpdateCommand(invoiceLine), cancellationToken);
        return RedirectToAction(nameof(Edit), new { id = invoiceLine.InvoiceId });
    }
}