using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderAttachmentQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.FloorOrderAttachmentQuery, IEnumerable<FloorOrderAttachment>>
{
    public Task<IEnumerable<FloorOrderAttachment>> Handle(Queries.FloorOrderAttachmentQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty) return Task.FromResult(Enumerable.Empty<FloorOrderAttachment>());

        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderAttachment} WHERE {DbConstants.Columns.FloorOrderId} = @Id ORDER BY {DbConstants.Columns.CreatedAt} DESC";
        return dapperContext.Connection.QueryAsync<FloorOrderAttachment>(query, new { request.Id });
    }
}
