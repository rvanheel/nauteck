using MediatR;

namespace nauteck.core.Features.Agenda;

public static class Queries
{
    public sealed record AgendaQuery : IRequest<IEnumerable<data.Entities.Agenda.Agenda>>;
}