using MediatR;

namespace nauteck.core.Features.Client;

public static class Commands
{
    public sealed record ClientDeleteCommand(Guid Id) : IRequest;
    public sealed record ClientUpdateCommand(data.Models.Client.ClientPostModel ClientPostModel, string UserName, DateTime Timestamp) : IRequest;
}