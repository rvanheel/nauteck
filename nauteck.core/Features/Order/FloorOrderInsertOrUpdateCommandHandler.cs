using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Models.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderInsertOrUpdateCommandHandler(IDbConnection dbConnection, IHelper helper) : IRequestHandler<Commands.FloorOrderInsertOrUpdateCommand>
{
    public async Task Handle(Commands.FloorOrderInsertOrUpdateCommand request, CancellationToken cancellationToken)
    {
        await Insert(request.OrderPostModel, request.UserName, request.DealerId);
        await Update(request.OrderPostModel, request.UserName);
        await Logos(request.OrderPostModel);
        await Parts(request.OrderPostModel);
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
    private async Task Insert(OrderPostModel model, string userName, string dealerId)
    {
        if (!model.Id!.Equals(Guid.Empty.ToString())) return;
        model.Id = Guid.NewGuid().ToString();
        var now = helper.AtCurrentTimeZone;
        var sql = $@"INSERT INTO FloorOrder 
        (
            {DbConstants.Columns.Id}, 
            {DbConstants.Columns.Reference},
            {DbConstants.Columns.LastName},
            {DbConstants.Columns.FirstName},
            {DbConstants.Columns.Preamble},
            {DbConstants.Columns.Phone},
            {DbConstants.Columns.Email},
            {DbConstants.Columns.Address},
            {DbConstants.Columns.Number},
            {DbConstants.Columns.Extension},
            {DbConstants.Columns.Zipcode},
            {DbConstants.Columns.City},
            {DbConstants.Columns.Region},
            {DbConstants.Columns.Country},
            {DbConstants.Columns.BoatBrand},
            {DbConstants.Columns.BoatType},
            {DbConstants.Columns.CreatedAt},
            {DbConstants.Columns.CreatedBy},
            {DbConstants.Columns.ModifiedAt},
            {DbConstants.Columns.ModifiedBy},
            {DbConstants.Columns.Active},
            {DbConstants.Columns.Discount},
            {DbConstants.Columns.Total},
            {DbConstants.Columns.Status},
            {DbConstants.Columns.StatusAction},
            {DbConstants.Columns.Comment},
            {DbConstants.Columns.FreeText},
            {DbConstants.Columns.DealerId},
            {DbConstants.Columns.Infix},
            {DbConstants.Columns.FreePrice},
            {DbConstants.Columns.FreePriceText},
            {DbConstants.Columns.Provision},
            {DbConstants.Columns.ProvisionPercentage},
            {DbConstants.Columns.ConstructionBy},
            {DbConstants.Columns.InvoiceFreeText},
            {DbConstants.Columns.CompanyName},
            {DbConstants.Columns.VatNumber}
        VALUES (
            @Id,
            @Reference,
            @LastName,
            @FirstName,
            @Preamble,
            @Phone,
            @Email,
            @Address,
            @Number,
            @Extension,
            @Zipcode,
            @City,
            @Region,
            @Country,
            @BoatBrand,
            @BoatType,
            @CreatedAt,
            @CreatedBy,
            @ModifiedAt,
            @ModifiedBy,
            1,
            @Discount,
            @Total,
            @Status,
            @StatusAction,
            @Comment,
            @FreeText,
            @DealerId,
            @Infix,
            @FreePrice,
            @FreePriceText,
            @Provision,
            @ProvisionPercentage,
            @ConstructionBy,
            @InvoiceFreeText,
            @CompanyName,
            @VatNumber
        )";
        _ = await dbConnection.ExecuteAsync(sql, new
        {
            model.Id,
            model.Reference,
            LastName = model.LastName ?? "",
            FirstName = model.FirstName ?? "",
            Preamble = model.Preamble ?? "",
            Phone = model.Phone ?? "",
            Email = model.Email ?? "",
            Address = model.Address ?? "",
            Number = model.Number ?? "",
            Extension = model.Extension ?? "",
            Zipcode = model.Zipcode ?? "",
            City = model.City ?? "",
            Region = model.Region ?? "",
            Country = model.Country ?? "",
            BoatBrand = model.BoatBrand ?? "",
            BoatType = model.BoatType ?? "",
            CreatedAt = now,
            CreatedBy = userName,
            ModifiedAt = now,
            ModifiedBy = userName,
            model.Discount,
            model.Total,
            model.Status,
            model.StatusAction,
            model.Comment,
            model.FreeText,
            DealerId = dealerId,
            Infix = model.Infix ?? "",
            model.FreePrice,
            FreePriceText = model.FreePriceText ?? "",
            model.Provision,
            ProvisionPercentage = 0,
            ConstructionBy = model.ConstructionBy ?? "",
            InvoiceFreeText = model.InvoiceFreeText ?? "",
            CompanyName = model.CompanyName ?? "",
            VatNumber = model.VatNumber ?? ""
        });
    }
    private async Task Logos(OrderPostModel model)
    {
        // delete old logos
        var sql = $"DELETE FROM {DbConstants.Tables.FloorOrderLogo} WHERE {DbConstants.Columns.FloorOrderId} = @FloorOrderId";
        _ = await dbConnection.ExecuteAsync(sql, new { FloorOrderId = model.Id });

        sql = $@"INSERT INTO {DbConstants.Tables.FloorOrderLogo}
            ({DbConstants.Columns.Id}
            ,{DbConstants.Columns.Price}
            ,{DbConstants.Columns.Quantity}
            ,{DbConstants.Columns.FloorOrderId}
            ,{DbConstants.Columns.Description}) 
            VALUES (UUID(),@Price,@Quantity,@FloorOrderId,@Description)";
        
        foreach (data.Entities.Order.FloorOrderLogo? logo in model.Logo.Where(l=> l.Quantity > 0))
        {
            await dbConnection.ExecuteAsync(sql, new
            {
                logo!.Price,
                logo.Quantity,
                FloorOrderId = model.Id,
                logo.Description
            });
        }
        
    }
    private async Task Parts(OrderPostModel model)
    {
        // delete old parts
        var sql = $"DELETE FROM {DbConstants.Tables.FloorOrderParts} WHERE {DbConstants.Columns.FloorOrderId} = @FloorOrderId";
        _ = await dbConnection.ExecuteAsync(sql, new { FloorOrderId = model.Id });

        if (model.Parts is null) return;
        model.Parts.FloorOrderId = Guid.Parse(model.Id!);
        sql = $@"INSERT INTO {DbConstants.Tables.FloorOrderParts}
           ({DbConstants.Columns.Id}
            ,{DbConstants.Columns.Floor}
            ,{DbConstants.Columns.FloorPrice}
            ,{DbConstants.Columns.FloorQuantity}
            ,{DbConstants.Columns.FloorColorAbove}
            ,{DbConstants.Columns.FloorColorBeneath}
            ,{DbConstants.Columns.FloorTotal}
            ,{DbConstants.Columns.Design}
            ,{DbConstants.Columns.DesignPrice}
            ,{DbConstants.Columns.DesignTotal}
            ,{DbConstants.Columns.Measurement}
            ,{DbConstants.Columns.MeasurementPrice}
            ,{DbConstants.Columns.MeasurementTotal}
            ,{DbConstants.Columns.Construction}
            ,{DbConstants.Columns.ConstructionPrice}
            ,{DbConstants.Columns.ConstructionTotal}
            ,{DbConstants.Columns.CallOutCostPrice}
            ,{DbConstants.Columns.CallOutCostTotal}
            ,{DbConstants.Columns.FloorOrderId}
            ,{DbConstants.Columns.CallOutCostQuantity}
            ,{DbConstants.Columns.ColorPrice}
            ,{DbConstants.Columns.ColorTotal}
            ,{DbConstants.Columns.LogoTotal}
            ,{DbConstants.Columns.FloorColorExclusive}
            )
            VALUES (UUID()
            ,@Floor
            ,@FloorPrice
            ,@FloorQuantity
            ,@FloorColorAbove
            ,@FloorColorBeneath
            ,@FloorTotal
            ,@Design
            ,@DesignPrice
            ,@DesignTotal
            ,@Measurement
            ,@MeasurementPrice
            ,@MeasurementTotal
            ,@Construction
            ,@ConstructionPrice
            ,@ConstructionTotal
            ,@CallOutCostPrice
            ,@CallOutCostTotal
            ,@FloorOrderId
            ,@CallOutCostQuantity
            ,@ColorPrice
            ,@ColorTotal
            ,@LogoTotal
            ,@FloorColorExclusive
            )";
        await dbConnection.ExecuteAsync(sql, model.Parts);
    }
    private async Task Update(OrderPostModel model, string userName)
    {
        if (model.Id!.Equals(Guid.Empty.ToString())) return;

        model.Provision = model.Status!.Equals(Constants.Status.CANCELLED) ? 0 : model.Provision;
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
            ,{DbConstants.Columns.ModifiedAt}=@ModifiedAt
            ,{DbConstants.Columns.ModifiedBy}=@ModifiedBy
        WHERE {DbConstants.Columns.Id} = @Id";
        _ = await dbConnection.ExecuteAsync(sql, new
        {
            Id = model.Id ?? "",
            ModifiedAt = helper.AtCurrentTimeZone,
            ModifiedBy = userName,
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
