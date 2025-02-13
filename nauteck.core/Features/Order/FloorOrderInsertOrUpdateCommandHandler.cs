using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Models.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderInsertOrUpdateCommandHandler(IDbConnection dbConnection) : IRequestHandler<Commands.FloorOrderInsertOrUpdateCommand>
{
    public async Task Handle(Commands.FloorOrderInsertOrUpdateCommand request, CancellationToken cancellationToken)
    {
        await Update(request.OrderPostModel);
    }

    #region Private Methods
    private async Task Update(OrderPostModel model)
    {
        var sql = $@"
        UPDATE {DbConstants.Tables.FloorOrder} 
        SET 
            {DbConstants.Columns.FirstName}=@FirstName  
            ,{DbConstants.Columns.Infix}=@Infix
            ,{DbConstants.Columns.LastName}=@LastName
            ,{DbConstants.Columns.Preamble}=@Preamble
            ,{DbConstants.Columns.Address}=@Address
            ,{DbConstants.Columns.City} = @City
            ,{DbConstants.Columns.Country}=@Country
            ,{DbConstants.Columns.Extension}=@Extension
            ,{DbConstants.Columns.Number}=@Number
            ,{DbConstants.Columns.Region}=@Region 
            ,{DbConstants.Columns.Zipcode}=@Zipcode
            ,{DbConstants.Columns.BoatBrand}=@BoatBrand
            ,{DbConstants.Columns.BoatType}=@BoatType
            ,{DbConstants.Columns.CompanyName}=@CompanyName
            ,{DbConstants.Columns.VatNumber}=@VatNumber
        WHERE {DbConstants.Columns.Id} = @Id";
        _ = await dbConnection.ExecuteAsync(sql, new
        {
            Id = model.Id ?? "",
            CompanyName = model.CompanyName ?? "",
            VatNumber = model.VatNumber ?? "",
            // personalia
            FirstName = model.FirstName ?? "",
            Infix = model.Infix ?? "",
            LastName = model.LastName ?? "",
            Preamble = model.Preamble ?? "",
            // address
            Address = model.Address ?? "",
            City = model.City ?? "",
            Country = model.Country ?? "",
            Extension = model.Extension ?? "",
            Number = model.Number ?? "",
            Region = model.Region ?? "",
            Zipcode = model.Zipcode ?? "",
            // boat
            BoatBrand = model.BoatBrand ?? "",
            BoatType = model.BoatType ?? ""
        });
    }
    #endregion
}
