using MediatR;

using nauteck.data.Dto.Order;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public static class Queries
{
    public sealed record FloorOrderAttachmentQuery(Guid Id) : IRequest<IEnumerable<FloorOrderAttachment>>;
    public sealed record FloorOrderQuery : IRequest<IEnumerable<FloorOrderDto>>;
    public sealed record FloorOrderByIdQuery(string Id) : IRequest<FloorOrder>;
    public sealed record FloorOrderLogoQuery(Guid Id) : IRequest<IEnumerable<FloorOrderLogo>>;
    public sealed record FloorOrderPartQuery(Guid Id) : IRequest<FloorOrderPart?>;
    public sealed record FloorOrderStatusQuery(Guid Id) : IRequest<IEnumerable<FloorOrderStatus>>;
}
