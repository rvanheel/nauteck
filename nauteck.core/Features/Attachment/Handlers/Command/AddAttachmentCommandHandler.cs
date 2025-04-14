using nauteck.core.Implementation;

using static nauteck.core.Features.Attachment.Commands;

namespace nauteck.core.Features.Attachment.Handlers.Command;

public sealed class AddAttachmentCommandHandler(IDapperContext dapperContext, IHelper helper) : IRequestHandler<AddAttachmentCommand>
{
    public async Task Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
    {
        await dapperContext.Connection.ExecuteAsync($@"INSERT INTO {DbConstants.Tables.Attachment} (
        {DbConstants.Columns.Id}
        , {DbConstants.Columns.FileName}
        , {DbConstants.Columns.FileUrl}
        , {DbConstants.Columns.FileSize}
        , {DbConstants.Columns.Type}
        , {DbConstants.Columns.CreatedAt}
        , {DbConstants.Columns.CreatedBy}
        , {DbConstants.Columns.ClientId}
        ) VALUES (
            UUID()
            , @FileName
            , @FileUrl
            , @FileSize
            , @Type
            , @CreatedAt
            , @CreatedBy
            , @ClientId
        )", new 
        {
            request.AttachmentPostModel.FileName,
            FileUrl = request.AttachmentPostModel.BlobUri?.ToString(),
            FileSize = request.AttachmentPostModel.Size,
            Type = request.AttachmentPostModel.ContentType,
            CreatedAt = helper.AtCurrentTimeZone,
            CreatedBy = request.UserName,
            request.AttachmentPostModel.ClientId
        }, commandType: CommandType.Text);
    }
}
