using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Attachment;

using static nauteck.core.Features.Attachment.Queries;

namespace nauteck.core.Features.Attachment.Handlers.Query;

public sealed class AttachmentByClientIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<AttachmentByClientIdQuery, IEnumerable<AttachmentDto>>
{
    public Task<IEnumerable<AttachmentDto>> Handle(AttachmentByClientIdQuery request, CancellationToken cancellationToken)
    {
        return request.Id.Equals(Guid.Empty)
            ? Task.FromResult(Enumerable.Empty<AttachmentDto>())
            : dapperContext.Connection.QueryAsync<AttachmentDto>(
                $"SELECT * FROM {DbConstants.Tables.Attachment} WHERE ClientId = @Id", request);
    }
}
