using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed record FloorOrderByIdQuery(string Id) : IRequest<FloorOrder>;

public sealed class FloorOrderByIdQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorOrderByIdQuery, FloorOrder>
{
    private readonly string Columns = @"
        CAST(`Id` AS NCHAR) AS `Id`
        ,`Reference`
        ,`LastName`
        ,`FirstName`
        ,`Preamble`
        ,`Phone`
        ,`Email`
        ,`Address`
        ,`Number`
        ,`Extension`
        ,`Zipcode`
        ,`City`
        ,`Region`
        ,`Country`
        ,`BoatBrand`
        ,`BoatType`
        ,`CreatedAt`
        ,`CreatedBy`
        ,`ModifiedAt`
        ,`ModifiedBy`
        ,`Active`
        ,`Discount`
        ,`Total`
        ,`Status`
        ,`StatusAction`
        ,`Comment`
        ,`FreeText`
        ,`InvoiceFreeText`
        ,`DealerId`
        ,`Infix`
        ,`FreePrice`
        ,`FreePriceText`
        ,`Provision`
        ,`ProvisionPercentage`
        ,`ConstructionBy`
        ,`CompanyName`
        ,`VatNumber`";
	
    public Task<FloorOrder> Handle(FloorOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryFirstAsync<FloorOrder>($"SELECT {Columns} FROM {DbConstants.Tables.FloorOrder} WHERE `Id` = @Id", new { request.Id });
    }
}