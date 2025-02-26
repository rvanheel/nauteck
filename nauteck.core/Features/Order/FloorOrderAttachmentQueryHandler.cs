using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderAttachmentQueryHandler(IDbConnection dbConnection) : IRequestHandler<Queries.FloorOrderAttachmentQuery, IEnumerable<FloorOrderAttachment>>
{
    public Task<IEnumerable<FloorOrderAttachment>> Handle(Queries.FloorOrderAttachmentQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return Task.FromResult(Enumerable.Empty<FloorOrderAttachment>());

        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderAttachment} WHERE {DbConstants.Columns.FloorOrderId} = @Id ORDER BY {DbConstants.Columns.CreatedAt} DESC";
        return dbConnection.QueryAsync<FloorOrderAttachment>(query, new { request.Id });
    }
}
