using nauteck.core.Implementation;

using static nauteck.core.Features.User.Commands;


namespace nauteck.core.Features.User.Handlers.Command;

public sealed class DeleteUserCommandHandler(IDapperContext dapperContext) : IRequestHandler<DeleteUserCommand>
{
    public Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.User} WHERE {DbConstants.Columns.Id} = @Id", new { request.Id });
    }
}
