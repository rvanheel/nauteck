using nauteck.data.Dto.Invoice;
using nauteck.data.Dto.Quotation;
using nauteck.data.Entities.Floor.Color;

namespace nauteck.core.Features;

public static class Queries
{
    public static class Client
    {
        public sealed record ClientByIdQuery(Guid Id) : IRequest<ClientDto>;
    }
    public static class Floor
    {
        public sealed record FloorColorQuery : IRequest<IEnumerable<FloorColor>?>;
        public sealed record FloorColorExclusiveQuery : IRequest<IEnumerable<FloorColorExclusive>?>;
        public sealed record FloorQuery() : IRequest<IEnumerable<data.Entities.Floor.Floor>?>;
    }
    public static class Invoice
    {
        public sealed record InvoiceByIdQuery(Guid Id) : IRequest<InvoiceDto>;
        public sealed record InvoicePdfQuery(Guid Id) : IRequest<(BinaryData, string)>;
        public sealed record InvoiceQuery : IRequest<IEnumerable<InvoiceDto>>;
        public sealed record InvoiceLineQuery(Guid Id) : IRequest<IEnumerable<InvoiceLineDto>>;
    }
    public static class Quotation
    {
        public sealed record QuotationByIdQuery(Guid Id) : IRequest<QuotationDto>;
        public sealed record QuotationPdfQuery(Guid Id) : IRequest<(BinaryData, string)>;
        public sealed record QuotationQuery : IRequest<IEnumerable<QuotationDto>>;
        public sealed record QuotationLineQuery(Guid Id) : IRequest<IEnumerable<QuotationLineDto>>;
    }
}
