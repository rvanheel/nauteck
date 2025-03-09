using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Attachment.Commands;

namespace nauteck.core.Features.Attachment.Handlers.Command;

public sealed class DeleteAttachmentCommandHandler(IDapperContext dapperContext, IBlobStorage blobStorage) : IRequestHandler<DeleteAttachmentCommand>
{
    public async Task Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = dapperContext.Connection.QueryFirstOrDefault<string?>($"SELECT {DbConstants.Columns.FileName} FROM {DbConstants.Tables.Attachment} WHERE Id = @Id", new { request.Id });
        var t1 = blobStorage.DeleteBlobByFileName(attachment, cancellationToken);
        var t2 = dapperContext.Connection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.Attachment} WHERE Id = @Id", new { request.Id });
        await Task.WhenAll(t1, t2);
    }
}
