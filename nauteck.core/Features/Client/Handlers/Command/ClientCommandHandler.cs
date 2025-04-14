using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Command;

public sealed class ClientCommandHandler(IDapperContext dapperContext) : IRequestHandler<Commands.ClientUpdateCommand>
{
    public async Task Handle(Commands.ClientUpdateCommand request, CancellationToken cancellationToken)
    {
        await Insert(request);
        await Update(request);
    }

    #region Private Methods 
    private async Task Insert(Commands.ClientUpdateCommand request)
    {
        if (request.ClientPostModel.Id != Guid.Empty) return;

        request.ClientPostModel.Id = Guid.NewGuid();
        request.ClientPostModel.CreatedAt = request.Timestamp;
        request.ClientPostModel.CreatedBy = request.UserName;
        request.ClientPostModel.ModifiedAt = request.Timestamp;
        request.ClientPostModel.ModifiedBy = request.UserName;
        var sql = $@"INSERT INTO {DbConstants.Tables.Client.TableName} 
            ({DbConstants.Tables.Client.Columns.Id}
            ,{DbConstants.Tables.Client.Columns.Preamble}
            ,{DbConstants.Tables.Client.Columns.LastName}
            ,{DbConstants.Tables.Client.Columns.FirstName}
            ,{DbConstants.Tables.Client.Columns.Infix}
            ,{DbConstants.Tables.Client.Columns.Phone}
            ,{DbConstants.Tables.Client.Columns.Email}
            ,{DbConstants.Tables.Client.Columns.Address}
            ,{DbConstants.Tables.Client.Columns.Number}
            ,{DbConstants.Tables.Client.Columns.Extension}
            ,{DbConstants.Tables.Client.Columns.Zipcode}
            ,{DbConstants.Tables.Client.Columns.City}
            ,{DbConstants.Tables.Client.Columns.Region}
            ,{DbConstants.Tables.Client.Columns.Country}
            ,{DbConstants.Tables.Client.Columns.BoatBrand}
            ,{DbConstants.Tables.Client.Columns.BoatType}
            ,{DbConstants.Tables.Client.Columns.Remarks}
            ,{DbConstants.Tables.Client.Columns.Active}
            ,{DbConstants.Tables.Client.Columns.CreatedAt}
            ,{DbConstants.Tables.Client.Columns.CreatedBy}
            ,{DbConstants.Tables.Client.Columns.ModifiedAt}
            ,{DbConstants.Tables.Client.Columns.ModifiedBy}
            )
            VALUES
            (@{nameof(data.Entities.Client.Client.Id)}
            ,@{nameof(data.Entities.Client.Client.Preamble)}
            ,@{nameof(data.Entities.Client.Client.LastName)}
            ,@{nameof(data.Entities.Client.Client.FirstName)}
            ,@{nameof(data.Entities.Client.Client.Infix)}
            ,@{nameof(data.Entities.Client.Client.Phone)}
            ,@{nameof(data.Entities.Client.Client.Email)}
            ,@{nameof(data.Entities.Client.Client.Address)}
            ,@{nameof(data.Entities.Client.Client.Number)}
            ,@{nameof(data.Entities.Client.Client.Extension)}
            ,@{nameof(data.Entities.Client.Client.Zipcode)}
            ,@{nameof(data.Entities.Client.Client.City)}
            ,@{nameof(data.Entities.Client.Client.Region)}
            ,@{nameof(data.Entities.Client.Client.Country)}
            ,@{nameof(data.Entities.Client.Client.BoatBrand)}
            ,@{nameof(data.Entities.Client.Client.BoatType)}
            ,@{nameof(data.Entities.Client.Client.Remarks)}
            ,@{nameof(data.Entities.Client.Client.Active)}
            ,@{nameof(data.Entities.Client.Client.CreatedAt)}
            ,@{nameof(data.Entities.Client.Client.CreatedBy)}
            ,@{nameof(data.Entities.Client.Client.ModifiedAt)}
            ,@{nameof(data.Entities.Client.Client.ModifiedBy)}
        )";
        await dapperContext.Connection.ExecuteAsync(sql, request.ClientPostModel);
    }

    private async Task Update(Commands.ClientUpdateCommand request)
    {
        if (request.ClientPostModel.Id == Guid.Empty) return;

        request.ClientPostModel.ModifiedAt = request.Timestamp;
        request.ClientPostModel.ModifiedBy = request.UserName;

        var sql = $@"UPDATE {DbConstants.Tables.Client.TableName}
        SET
        {DbConstants.Columns.Preamble} = @{nameof(data.Entities.Client.Client.Preamble)}    
        ,{DbConstants.Tables.Client.Columns.LastName} = @{nameof(data.Entities.Client.Client.LastName)}
        ,{DbConstants.Tables.Client.Columns.FirstName} = @{nameof(data.Entities.Client.Client.FirstName)}
        ,{DbConstants.Tables.Client.Columns.Infix} = @{nameof(data.Entities.Client.Client.Infix)}
        ,{DbConstants.Tables.Client.Columns.Phone} = @{nameof(data.Entities.Client.Client.Phone)}
        ,{DbConstants.Tables.Client.Columns.Email} = @{nameof(data.Entities.Client.Client.Email)}
        ,{DbConstants.Tables.Client.Columns.Address} = @{nameof(data.Entities.Client.Client.Address)}
        ,{DbConstants.Tables.Client.Columns.Number} = @{nameof(data.Entities.Client.Client.Number)}
        ,{DbConstants.Tables.Client.Columns.Extension} = @{nameof(data.Entities.Client.Client.Extension)}
        ,{DbConstants.Tables.Client.Columns.Zipcode} = @{nameof(data.Entities.Client.Client.Zipcode)}
        ,{DbConstants.Tables.Client.Columns.City} = @{nameof(data.Entities.Client.Client.City)}
        ,{DbConstants.Tables.Client.Columns.Region} = @{nameof(data.Entities.Client.Client.Region)}
        ,{DbConstants.Tables.Client.Columns.Country} = @{nameof(data.Entities.Client.Client.Country)}
        ,{DbConstants.Tables.Client.Columns.BoatBrand} = @{nameof(data.Entities.Client.Client.BoatBrand)}
        ,{DbConstants.Tables.Client.Columns.BoatType} = @{nameof(data.Entities.Client.Client.BoatType)}
        ,{DbConstants.Tables.Client.Columns.Remarks} = @{nameof(data.Entities.Client.Client.Remarks)}
        ,{DbConstants.Tables.Client.Columns.Active} = @{nameof(data.Entities.Client.Client.Active)}
        ,{DbConstants.Tables.Client.Columns.ModifiedAt} = @{nameof(data.Entities.Client.Client.ModifiedAt)}
        ,{DbConstants.Tables.Client.Columns.ModifiedBy} = @{nameof(data.Entities.Client.Client.ModifiedBy)}
        WHERE {DbConstants.Columns.Id} = @{nameof(data.Entities.Client.Client.Id)}";
        await dapperContext.Connection.ExecuteAsync(sql, request.ClientPostModel);
    }
    #endregion
}
