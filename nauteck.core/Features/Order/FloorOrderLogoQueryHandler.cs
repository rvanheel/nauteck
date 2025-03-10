using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

using static nauteck.core.Features.Order.Queries;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderLogoQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorOrderLogoQuery, IEnumerable<FloorOrderLogo>>
{
    public Task<IEnumerable<FloorOrderLogo>> Handle(FloorOrderLogoQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return Task.FromResult(Enumerable.Empty<FloorOrderLogo>());
        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderLogo} WHERE {DbConstants.Columns.FloorOrderId} = @Id";
        return dapperContext.Connection.QueryAsync<FloorOrderLogo>(query, new { request.Id });
    }
}
