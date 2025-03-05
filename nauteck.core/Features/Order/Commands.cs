using MediatR;

using nauteck.data.Models.Order;

namespace nauteck.core.Features.Order;

public static class Commands
{
    public sealed record FloorOrderDeleteCommand(string Id) : IRequest;
    public sealed record FloorOrderInsertOrUpdateCommand(OrderPostModel OrderPostModel, DateTime Timestamp, string UserName, string DealerId, string? Reference) : IRequest;
}