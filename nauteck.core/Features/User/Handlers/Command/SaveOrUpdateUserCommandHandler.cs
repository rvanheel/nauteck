using nauteck.core.Implementation;

using static nauteck.core.Features.User.Commands;

namespace nauteck.core.Features.User.Handlers.Command;

public sealed class SaveOrUpdateUserCommandHandler(IDapperContext dapperContext, IHelper helper) : IRequestHandler<SaveOrUpdateUserCommand>
{
    public async Task Handle(SaveOrUpdateUserCommand request, CancellationToken cancellationToken)
    {
        await Insert(request);
        await Update(request);
    }

    #region Private Methods
    private async Task Insert(SaveOrUpdateUserCommand request)
    {
        if (!request.User.Id.Equals(Guid.Empty)) return;
        var now = helper.AtCurrentTimeZone;
        request.User.CreatedBy = request.UserName;
        request.User.CreatedAt = now;
        request.User.ModifiedBy = request.UserName;
        request.User.ModifiedAt = now;
        request.User.ActivationCode = Guid.Empty;
        request.User.DealerId = request.DealerId;
        request.User.Password = helper.GenerateHashedPassword(request.User.Password ?? "abcd1234#");
        var sql = $@"INSERT INTO {DbConstants.Tables.User}(
            {DbConstants.Columns.Id}
            , {DbConstants.Columns.FirstName}
            ,{DbConstants.Columns.Infix}
            ,{DbConstants.Columns.LastName}
            ,{DbConstants.Columns.Preamble}
            ,{DbConstants.Columns.Email}
            ,{DbConstants.Columns.Password}
            ,{DbConstants.Columns.Description}
            ,{DbConstants.Columns.Roles}
            ,{DbConstants.Columns.Active}
            ,{DbConstants.Columns.DealerId}
            ,{DbConstants.Columns.CreatedAt}
            ,{DbConstants.Columns.CreatedBy}
            ,{DbConstants.Columns.ModifiedAt}
            ,{DbConstants.Columns.ModifiedBy},
            {DbConstants.Columns.ActivationCode}
            )
            VALUES (
            UUID()
            ,@{nameof(DbConstants.Columns.FirstName)}
            ,@{nameof(DbConstants.Columns.Infix)}
            ,@{nameof(DbConstants.Columns.LastName)}
            ,@{nameof(DbConstants.Columns.Preamble)}
            ,@{nameof(DbConstants.Columns.Email)}
            ,@{nameof(DbConstants.Columns.Password)}
            ,@{nameof(DbConstants.Columns.Description)}
            ,@{nameof(DbConstants.Columns.Roles)}
            ,@{nameof(DbConstants.Columns.Active)}
            ,@{nameof(DbConstants.Columns.DealerId)}
            ,@{nameof(DbConstants.Columns.CreatedAt)}
            ,@{nameof(DbConstants.Columns.CreatedBy)}
            ,@{nameof(DbConstants.Columns.ModifiedAt)}
            ,@{nameof(DbConstants.Columns.ModifiedBy)}
            ,@{nameof(DbConstants.Columns.ActivationCode)}
            );";
        await dapperContext.Connection.ExecuteAsync(sql, request.User);
    }
    private async Task Update(SaveOrUpdateUserCommand request)
    {
        if (request.User.Id.Equals(Guid.Empty)) return;
        var now = helper.AtCurrentTimeZone;
        request.User.ModifiedBy = request.UserName;
        request.User.ModifiedAt = now;
        if (!string.Equals(request.User.Password, request.User.PasswordHash, StringComparison.Ordinal)) request.User.Password = helper.GenerateHashedPassword(request.User.Password) ?? "";
        var sql = $@"UPDATE {DbConstants.Tables.User}
        SET
        {DbConstants.Columns.FirstName} = @{nameof(DbConstants.Columns.FirstName)}
        ,{DbConstants.Columns.Infix} = COALESCE(@{nameof(DbConstants.Columns.Infix)}, '')
        ,{DbConstants.Columns.LastName} = COALESCE(@{nameof(DbConstants.Columns.LastName)}, '')
        ,{DbConstants.Columns.Preamble} = @{nameof(DbConstants.Columns.Preamble)}
        ,{DbConstants.Columns.Email} = @{nameof(DbConstants.Columns.Email)}
        ,{DbConstants.Columns.Password} = @{nameof(DbConstants.Columns.Password)}
        ,{DbConstants.Columns.Description} = COALESCE(@{nameof(DbConstants.Columns.Description)}, '')
        ,{DbConstants.Columns.Roles} = @{nameof(DbConstants.Columns.Roles)}
        ,{DbConstants.Columns.Active} = @{nameof(DbConstants.Columns.Active)}
        ,{DbConstants.Columns.ModifiedAt} = @{nameof(DbConstants.Columns.ModifiedAt)}
        ,{DbConstants.Columns.ModifiedBy} = @{nameof(DbConstants.Columns.ModifiedBy)}
        
        WHERE {DbConstants.Columns.Id} = @{nameof(DbConstants.Columns.Id)};";
        await dapperContext.Connection.ExecuteAsync(sql, request.User);
    }
    #endregion
}
