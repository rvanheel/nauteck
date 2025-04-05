using MediatR;

using nauteck.data.Dto;

namespace nauteck.core.Features;

public static class Commands
{
    public static class Quotation
    {
        public sealed record QuotationDeleteCommand(Guid Id) : IRequest<int>;
        public sealed record QuotationLineDeleteCommand(Guid Id) : IRequest<int>;
        public sealed record SaveOrUpdateQuotationCommand(QuotationDto Quotation) : IRequest;
        public sealed record SaveOrUpdateQuotationLineCommand(QuotationLineDto QuotationLine) : IRequest;
    }
}