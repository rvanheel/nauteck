using MediatR;

using nauteck.data.Models.Order;

namespace nauteck.core.Features.Order;

public static class Commands
{
    public sealed record FloorOrderInsertOrUpdateCommand(OrderPostModel OrderPostModel) : IRequest;
}