using nauteck.data.Dto.Invoice;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Features;

public static class Commands
{
    public static class Invoice
    {
        public sealed record InvoiceDeleteCommand(Guid Id) : IRequest<int>;
        public sealed record InvoiceLineDeleteCommand(Guid Id, Guid InvoiceId) : IRequest<int>;
        public sealed record InvoiceLineSaveOrUpdateCommand(InvoiceLineDto InvoiceLine) : IRequest;
        public sealed record SaveOrUpdateInvoiceCommand(InvoiceDto Invoice) : IRequest<Guid>;
    }
    public static class Quotation
    {
        public sealed record QuotationDeleteCommand(Guid Id) : IRequest<int>;
        public sealed record QuotationLineDeleteCommand(Guid Id, Guid QuotationId) : IRequest<int>;
        public sealed record QuotationLineSaveOrUpdateCommand(QuotationLineDto QuotationLine) : IRequest;
        public sealed record SaveOrUpdateQuotationCommand(QuotationDto Quotation) : IRequest<Guid>;
    }
}