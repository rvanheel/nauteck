using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Status.Commands;

namespace nauteck.core.Features.Status.Handlers.Command;

public sealed class SaveOrUpdateStatusCommandHandler(IDapperContext dapperContext, IHelper helper) : IRequestHandler<SaveOrUpdateStatusCommand>
{
    public Task Handle(SaveOrUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        request.Status.ModifiedAt = helper.AtCurrentTimeZone;
        request.Status.ModifiedBy = request.UserName;
        return dapperContext.Connection.ExecuteAsync($@"UPDATE {DbConstants.Tables.Status} SET 
        {DbConstants.Columns.Name} = @Name
        ,{DbConstants.Columns.Active} = @Active
        ,{DbConstants.Columns.ModifiedAt} = @ModifiedAt
        ,{DbConstants.Columns.ModifiedBy} = @ModifiedBy
        WHERE {DbConstants.Columns.Id} = @Id", request.Status);
    }
}
