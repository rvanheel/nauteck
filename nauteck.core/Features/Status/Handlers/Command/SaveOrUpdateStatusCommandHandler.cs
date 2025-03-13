using Dapper;

using MediatR;

using Mysqlx.Crud;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;

using static nauteck.core.Features.Status.Commands;

namespace nauteck.core.Features.Status.Handlers.Command;

public sealed class SaveOrUpdateStatusCommandHandler(IDapperContext dapperContext, IHelper helper) : IRequestHandler<SaveOrUpdateStatusCommand>
{
    public async Task Handle(SaveOrUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var now = helper.AtCurrentTimeZone;
        request.Status.CreatedAt = now;
        request.Status.CreatedBy = request.UserName;
        request.Status.ModifiedAt = now;
        request.Status.ModifiedBy = request.UserName;

        await Insert(request);
        await Update(request);
    }
    #region Private Methods
    private async Task Insert(SaveOrUpdateStatusCommand request)
    {
        if (!request.Status.Id.Equals(Guid.Empty)) return;
        
        
            
        await dapperContext.Connection.ExecuteAsync($@"INSERT INTO {DbConstants.Tables.Status} (
            {DbConstants.Columns.Id}
            , {DbConstants.Columns.Name}
            , {DbConstants.Columns.Active}
            , {DbConstants.Columns.CreatedAt}
            , {DbConstants.Columns.CreatedBy}
            , {DbConstants.Columns.ModifiedAt}
            , {DbConstants.Columns.ModifiedBy}
            ) VALUES (
            UUID()
            , @{nameof(DbConstants.Columns.Name)}
            , @{nameof(DbConstants.Columns.Active)}
            , @{nameof(DbConstants.Columns.CreatedAt)}
            , @{nameof(DbConstants.Columns.CreatedBy)}
            , @{nameof(DbConstants.Columns.ModifiedAt)}
            , @{nameof(DbConstants.Columns.ModifiedBy)})", request.Status);
    }

    private async Task Update(SaveOrUpdateStatusCommand request)
    {
        if (request.Status.Id.Equals(Guid.Empty)) return;
        
        request.Status.ModifiedAt = helper.AtCurrentTimeZone;
        request.Status.ModifiedBy = request.UserName;
        await dapperContext.Connection.ExecuteAsync($@"UPDATE {DbConstants.Tables.Status} 
            SET 
                {DbConstants.Columns.Name} = @{nameof(DbConstants.Columns.Name)}
                , {DbConstants.Columns.Active} = @{nameof(DbConstants.Columns.Active)}
                , {DbConstants.Columns.ModifiedAt} = @{nameof(DbConstants.Columns.ModifiedAt)}
                , {DbConstants.Columns.ModifiedBy} = @{nameof(DbConstants.Columns.ModifiedBy)}
            WHERE {DbConstants.Columns.Id} = @{nameof(DbConstants.Columns.Id)}", request.Status);

    }
    #endregion
}
