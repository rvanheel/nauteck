using MediatR;

namespace nauteck.core.Features.Status;

public static class Commands
{
    public sealed record SaveOrUpdateStatusCommand(data.Entities.Status.Status Status, string UserName) : IRequest;
}