using nauteck.data.Models.Attachment;

namespace nauteck.core.Features.Attachment;

public static class Commands
{
    public sealed record AddAttachmentCommand(AttachmentPostModel AttachmentPostModel, string UserName) : IRequest;
    public sealed record DeleteAttachmentCommand(Guid Id) : IRequest;
}