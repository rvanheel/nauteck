using System.Security.Policy;
using nauteck.core.Implementation;

using static nauteck.core.Features.Attachment.Commands;

namespace nauteck.core.Features.Attachment.Handlers.Command;

public sealed class DeleteAttachmentCommandHandler(IDapperContext dapperContext, IBlobStorage blobStorage) : IRequestHandler<DeleteAttachmentCommand>
{
    public async Task Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = dapperContext.Connection.QueryFirstOrDefault<string?>($"SELECT {DbConstants.Columns.FileUrl} FROM {DbConstants.Tables.Attachment} WHERE Id = @Id", new { request.Id });
        if (string.IsNullOrWhiteSpace(attachment)) return;
        var Url = new Uri(attachment);
        var fileName = Path.GetFileName(Url.LocalPath);
        var t1 = blobStorage.DeleteBlobByFileName(fileName, cancellationToken);
        var t2 = dapperContext.Connection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.Attachment} WHERE Id = @Id", new { request.Id });
        await Task.WhenAll(t1, t2);
    }
}
