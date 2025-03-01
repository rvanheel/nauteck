using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderPartQueryHandler(IDbConnection dbConnection) : IRequestHandler<Queries.FloorOrderPartQuery, FloorOrderPart>
{
    public async Task<FloorOrderPart> Handle(Queries.FloorOrderPartQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return new FloorOrderPart() { FloorOrderId = request.Id };
        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderParts} WHERE {DbConstants.Columns.FloorOrderId} = @Id LIMIT 1";
        var part = await dbConnection.QueryFirstOrDefaultAsync<FloorOrderPart>(query, new { request.Id });
        return part ?? new FloorOrderPart() { FloorOrderId = request.Id };
    }
}