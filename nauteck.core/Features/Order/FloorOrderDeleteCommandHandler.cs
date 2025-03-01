using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderDeleteCommandHandler(IDbConnection dbConnection, IBlobStorage blobStorage) : IRequestHandler<Commands.FloorOrderDeleteCommand>
{
    public async Task Handle(Commands.FloorOrderDeleteCommand request, CancellationToken cancellationToken)
    {

        // delete attachments
        var query = $"SELECT * FROM {DbConstants.Tables.FloorOrderAttachment} WHERE {DbConstants.Columns.FloorOrderId} = @Id";
        var attachments = await dbConnection.QueryAsync<FloorOrderAttachment>(query, new { request.Id });
        var tasks = attachments.Select(attachment => blobStorage.DeleteBlob(attachment.FileUrl, cancellationToken));
        await Task.WhenAll(tasks);

        query = $"DELETE FROM {DbConstants.Tables.FloorOrder} WHERE {DbConstants.Columns.Id} = @Id";
        await dbConnection.ExecuteAsync(query, new { request.Id });
    }
}
