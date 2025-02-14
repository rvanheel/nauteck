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
        await CheckStatus(request.OrderPostModel, request.Timestamp, request.UserName);
    }

    #region Private Methods
    private async Task CheckStatus(OrderPostModel orderPostModel, DateTime timestamp, string userName)
    {
        if ($"{orderPostModel.CurrentStatus}".Equals(orderPostModel.Status, StringComparison.InvariantCultureIgnoreCase)) return;
        var sql = $"INSERT INTO {DbConstants.Tables.FloorOrderStatus} ({DbConstants.Columns.Id},{DbConstants.Columns.Status},{DbConstants.Columns.CreatedAt},{DbConstants.Columns.CreatedBy},{DbConstants.Columns.FloorOrderId}) VALUES (@Id,@Status,@CreatedAt,@CreatedBy,@FloorOrderId)";
        _ = await dbConnection.ExecuteAsync(sql, new
        {
            Id = Guid.NewGuid(),
            orderPostModel.Status,
            CreatedAt = timestamp,
            CreatedBy = userName,
            FloorOrderId = orderPostModel.Id
        });
    }
    private async Task Update(OrderPostModel model)
    {
        if (model.Status == "Geannuleerd")
        {
            model.Provision = 0;
        }
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
            ,{DbConstants.Columns.Comment}=@Comment
            ,{DbConstants.Columns.Discount}=@Discount
            ,{DbConstants.Columns.InvoiceFreeText}=@InvoiceFreeText
            ,{DbConstants.Columns.FreeText}=@FreeText
            ,{DbConstants.Columns.FreePriceText}=@FreePriceText
            ,{DbConstants.Columns.FreePrice}=@FreePrice
            ,{DbConstants.Columns.Provision}=@Provision
            ,{DbConstants.Columns.Status}=@Status
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
            BoatType = model.BoatType ?? "",

            Comment = model.Comment ?? "",
            model.Discount,
                
            FreeText = model.FreeText ?? "",
            InvoiceFreeText = model.InvoiceFreeText ?? "",
            model.FreePrice,
            FreePriceText = model.FreePriceText ?? "",

            model.Provision,
            Status = model.Status ?? ""
        });
    }
    #endregion
}
