using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;

namespace nauteck.core.Features.Client.Handlers.Command;

public sealed class ClientCommandHandler(IDbConnection dbConnection) : IRequestHandler<Commands.ClientUpdateCommand>
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
        var sql = $@"INSERT INTO {DbConstants.Tables.Client} 
            ({DbConstants.Columns.Id}
            ,{DbConstants.Columns.Preamble}
            ,{DbConstants.Columns.LastName}
            ,{DbConstants.Columns.FirstName}
            ,{DbConstants.Columns.Infix}
            ,{DbConstants.Columns.Phone}
            ,{DbConstants.Columns.Email}
            ,{DbConstants.Columns.Address}
            ,{DbConstants.Columns.Number}
            ,{DbConstants.Columns.Extension}
            ,{DbConstants.Columns.Zipcode}
            ,{DbConstants.Columns.City}
            ,{DbConstants.Columns.Region}
            ,{DbConstants.Columns.Country}
            ,{DbConstants.Columns.BoatBrand}
            ,{DbConstants.Columns.BoatType}
            ,{DbConstants.Columns.Remarks}
            ,{DbConstants.Columns.Active}
            ,{DbConstants.Columns.CreatedAt}
            ,{DbConstants.Columns.CreatedBy}
            ,{DbConstants.Columns.ModifiedAt}
            ,{DbConstants.Columns.ModifiedBy}
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
        await dbConnection.ExecuteAsync(sql, request.ClientPostModel);
    }

    private async Task Update(Commands.ClientUpdateCommand request)
    {
        if (request.ClientPostModel.Id == Guid.Empty) return;

        request.ClientPostModel.ModifiedAt = request.Timestamp;
        request.ClientPostModel.ModifiedBy = request.UserName;

        var sql = $@"UPDATE {DbConstants.Tables.Client}
        SET
        {DbConstants.Columns.Preamble} = @{nameof(data.Entities.Client.Client.Preamble)}    
        ,{DbConstants.Columns.LastName} = @{nameof(data.Entities.Client.Client.LastName)}
        ,{DbConstants.Columns.FirstName} = @{nameof(data.Entities.Client.Client.FirstName)}
        ,{DbConstants.Columns.Infix} = @{nameof(data.Entities.Client.Client.Infix)}
        ,{DbConstants.Columns.Phone} = @{nameof(data.Entities.Client.Client.Phone)}
        ,{DbConstants.Columns.Email} = @{nameof(data.Entities.Client.Client.Email)}
        ,{DbConstants.Columns.Address} = @{nameof(data.Entities.Client.Client.Address)}
        ,{DbConstants.Columns.Number} = @{nameof(data.Entities.Client.Client.Number)}
        ,{DbConstants.Columns.Extension} = @{nameof(data.Entities.Client.Client.Extension)}
        ,{DbConstants.Columns.Zipcode} = @{nameof(data.Entities.Client.Client.Zipcode)}
        ,{DbConstants.Columns.City} = @{nameof(data.Entities.Client.Client.City)}
        ,{DbConstants.Columns.Region} = @{nameof(data.Entities.Client.Client.Region)}
        ,{DbConstants.Columns.Country} = @{nameof(data.Entities.Client.Client.Country)}
        ,{DbConstants.Columns.BoatBrand} = @{nameof(data.Entities.Client.Client.BoatBrand)}
        ,{DbConstants.Columns.BoatType} = @{nameof(data.Entities.Client.Client.BoatType)}
        ,{DbConstants.Columns.Remarks} = @{nameof(data.Entities.Client.Client.Remarks)}
        ,{DbConstants.Columns.Active} = @{nameof(data.Entities.Client.Client.Active)}
        ,{DbConstants.Columns.ModifiedAt} = @{nameof(data.Entities.Client.Client.ModifiedAt)}
        ,{DbConstants.Columns.ModifiedBy} = @{nameof(data.Entities.Client.Client.ModifiedBy)}
        WHERE {DbConstants.Columns.Id} = @{nameof(data.Entities.Client.Client.Id)}";
        await dbConnection.ExecuteAsync(sql, request.ClientPostModel);
    }
    #endregion
}
