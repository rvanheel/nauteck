using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Command;

public sealed class ClientDeleteCommandHandler(IDapperContext dapperContext, IBlobStorage blobStorage) : IRequestHandler<Commands.ClientDeleteCommand>
{
    public async Task Handle(Commands.ClientDeleteCommand request, CancellationToken cancellationToken)
    {
        var tasks = new List<Task>();
        var attachments = await dapperContext.Connection.QueryAsync<string>($"SELECT {DbConstants.Columns.FileName} FROM {DbConstants.Tables.Attachment} WHERE {DbConstants.Columns.ClientId} = @Id", new { request.Id });
        tasks.AddRange(attachments.Select(fileName => blobStorage.DeleteBlobByFileName(fileName, cancellationToken)));
        tasks.Add(dapperContext.Connection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.Attachment} WHERE {DbConstants.Columns.ClientId} = @Id", new { request.Id }));
        await Task.WhenAll(tasks);

        await dapperContext.Connection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.Client.TableName} WHERE {DbConstants.Columns.Id} = @Id", new { request.Id });
        
    }
}
