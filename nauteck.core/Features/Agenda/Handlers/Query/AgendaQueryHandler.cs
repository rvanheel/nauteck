using nauteck.core.Implementation;

namespace nauteck.core.Features.Agenda.Handlers.Query;

public sealed class AgendaQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.AgendaQuery, IEnumerable<data.Entities.Agenda.Agenda>>
{
    public Task<IEnumerable<data.Entities.Agenda.Agenda>> Handle(Queries.AgendaQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<data.Entities.Agenda.Agenda>($"SELECT * FROM {DbConstants.Tables.Agenda}");
    }
}