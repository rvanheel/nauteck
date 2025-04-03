using MediatR;

using nauteck.data.Dto;

namespace nauteck.core.Features;

public static class Queries
{
    public static class Quotation
    {
        public sealed record QuotationQuery : IRequest<IEnumerable<QuotationDto>>;
    }
}
