using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderStatusQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.FloorOrderStatusQuery, IEnumerable<FloorOrderStatus>>
{
    public Task<IEnumerable<FloorOrderStatus>> Handle(Queries.FloorOrderStatusQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return Task.FromResult(Enumerable.Empty<FloorOrderStatus>());

        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderStatus} WHERE {DbConstants.Columns.FloorOrderId} = @Id ORDER BY {DbConstants.Columns.CreatedAt} DESC";
        return dapperContext.Connection.QueryAsync<FloorOrderStatus>(query, new { request.Id });
    }
}
