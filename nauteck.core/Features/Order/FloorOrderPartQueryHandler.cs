using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderPartQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.FloorOrderPartQuery, FloorOrderPart>
{
    public async Task<FloorOrderPart> Handle(Queries.FloorOrderPartQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return new FloorOrderPart() { FloorOrderId = request.Id };
        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderParts.TableName} WHERE {DbConstants.Columns.FloorOrderId} = @Id LIMIT 1";
        var part = await dapperContext.Connection.QueryFirstOrDefaultAsync<FloorOrderPart>(query, new { request.Id });
        return part ?? new FloorOrderPart() { FloorOrderId = request.Id };
    }
}