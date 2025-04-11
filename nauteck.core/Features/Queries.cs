using MediatR;

using nauteck.data.Dto.Client;
using nauteck.data.Dto.Quotation;

namespace nauteck.core.Features;

public static class Queries
{
    public static class Client
    {
        public sealed record ClientByIdQuery(Guid Id) : IRequest<ClientDto>;
    }
    public static class Quotation
    {
        public sealed record QuotationByIdQuery(Guid Id) : IRequest<QuotationDto>;
        public sealed record QuotationPdfQuery(Guid Id) : IRequest<(BinaryData, string)>;
        public sealed record QuotationQuery : IRequest<IEnumerable<QuotationDto>>;
        public sealed record QuotationLineQuery(Guid Id) : IRequest<IEnumerable<QuotationLineDto>>;
    }
}
