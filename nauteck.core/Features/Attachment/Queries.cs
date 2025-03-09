using MediatR;

using nauteck.data.Dto.Attachment;

namespace nauteck.core.Features.Attachment;

public static class Queries
{
    public sealed record class AttachmentByClientIdQuery(Guid Id) : IRequest<IEnumerable<AttachmentDto>>;
}
