using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Status.Queries;

namespace nauteck.core.Features.Status.Handlers.Query;

public sealed class StatusByIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<StatusByIdQuery, data.Entities.Status.Status>
{
    public Task<data.Entities.Status.Status> Handle(StatusByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id.Equals(Guid.Empty)) return Task.FromResult(new data.Entities.Status.Status());

        return dapperContext.Connection.QueryFirstAsync<data.Entities.Status.Status>($"SELECT * FROM {DbConstants.Tables.Status} WHERE {DbConstants.Columns.Id} = @Id", new { request.Id });
    }
}