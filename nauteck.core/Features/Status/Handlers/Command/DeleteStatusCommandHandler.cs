using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Status.Commands;

namespace nauteck.core.Features.Status.Handlers.Command;

public sealed class DeleteStatusCommandHandler(IDapperContext dapperContext): IRequestHandler<DeleteStatusCommand>
{
    public Task Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.ExecuteAsync($@"DELETE FROM {DbConstants.Tables.Status} WHERE {DbConstants.Columns.Id} = @Id", request);
    }
}
