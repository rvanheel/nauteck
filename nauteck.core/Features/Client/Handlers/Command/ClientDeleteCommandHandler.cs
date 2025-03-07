using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Command;

public sealed class ClientDeleteCommandHandler(IDbConnection dbConnection) : IRequestHandler<Commands.ClientDeleteCommand>
{
    public Task Handle(Commands.ClientDeleteCommand request, CancellationToken cancellationToken)
    {
        return dbConnection.ExecuteAsync($"DELETE FROM {DbConstants.Tables.Client} WHERE {DbConstants.Columns.Id} = @Id", new { request.Id });
    }
}
