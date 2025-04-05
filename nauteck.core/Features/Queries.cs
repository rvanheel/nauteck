using MediatR;

using nauteck.data.Dto;
using nauteck.data.Dto.Client;

namespace nauteck.core.Features;

public static class Queries
{
    public static class Client
    {
        public sealed record ClientByIdQuery(Guid id) : IRequest<ClientDto>;
    }
    public static class Quotation
    {
        public sealed record QuotationByIdQuery(Guid Id) : IRequest<QuotationDto>;
        public sealed record QuotationQuery : IRequest<IEnumerable<QuotationDto>>;

        public sealed record QuotationLineQuery(Guid Id) : IRequest<IEnumerable<QuotationLineDto>>;
    }
}
