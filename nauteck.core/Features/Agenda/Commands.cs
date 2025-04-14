namespace nauteck.core.Features.Agenda;

public static class Commands
{
    public sealed record DeleteAgendaCommand(Guid Id) : IRequest;
    public sealed record class SaveOrUpdateAgendaCommand(data.Entities.Agenda.Agenda AgendaItem) : IRequest;
}