using MediatR;

namespace nauteck.core.Features.Agenda;

public static class Commands
{
    public sealed record DeleteAgendaCommand(Guid Id) : IRequest;
}