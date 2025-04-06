using MediatR;

using nauteck.data.Dto.Quotation;

namespace nauteck.core.Features;

public static class Commands
{
    public static class Quotation
    {
        public sealed record QuotationDeleteCommand(Guid Id) : IRequest<int>;
        public sealed record QuotationLineDeleteCommand(Guid Id, Guid QuotationId) : IRequest<int>;
        public sealed record QuotationLineSaveOrUpdateCommand(QuotationLineDto QuotationLine) : IRequest;
        public sealed record SaveOrUpdateQuotationCommand(QuotationDto Quotation) : IRequest<Guid>;
    }
}