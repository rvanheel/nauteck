using MediatR;

namespace nauteck.core.Features.Attachment;

public static class Commands
{
    public sealed record class DeleteAttachmentCommand(Guid Id) : IRequest;
}