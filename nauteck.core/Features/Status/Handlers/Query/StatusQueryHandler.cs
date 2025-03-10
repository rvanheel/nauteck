using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Status.Queries;

namespace nauteck.core.Features.Status.Handlers.Query;

public sealed class StatusQueryHandler(IDapperContext dapperContext) : IRequestHandler<StatusQuery, IEnumerable<data.Entities.Status.Status>>
{
    public Task<IEnumerable<data.Entities.Status.Status>> Handle(StatusQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<data.Entities.Status.Status>($"SELECT * FROM {DbConstants.Tables.Status}");
    }
}
