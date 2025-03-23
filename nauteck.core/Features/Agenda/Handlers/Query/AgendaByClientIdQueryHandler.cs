using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Agenda.Handlers.Query;

public sealed class AgendaByClientIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.AgendaByClientIdQuery, IEnumerable<data.Entities.Agenda.Agenda>>
{
    public Task<IEnumerable<data.Entities.Agenda.Agenda>> Handle(Queries.AgendaByClientIdQuery request, CancellationToken cancellationToken)
    {
        return request.Id == Guid.Empty ?
            Task.FromResult(Enumerable.Empty<data.Entities.Agenda.Agenda>()) :
            dapperContext.Connection.QueryAsync<data.Entities.Agenda.Agenda>(
                $"SELECT * FROM {DbConstants.Tables.Agenda} WHERE {DbConstants.Columns.ClientId} = @Id", request);
    }
}
