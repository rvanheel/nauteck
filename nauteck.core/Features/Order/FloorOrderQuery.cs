using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Dto.Order;

namespace nauteck.core.Features.Order;

public sealed record FloorOrderQuery : IRequest<IEnumerable<FloorOrderDto>>;

public sealed class FloorOrderQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorOrderQuery, IEnumerable<FloorOrderDto>>
{   

    private const string modelAndSupplierColumns = @"
        CAST(o.`Id` AS NCHAR) AS `Id`
        , o.`Reference`
        , o.`LastName`
        , o.`FirstName`
        , o.`Preamble`
        , o.`Infix`
        , o.`Address`
        , o.`Number`
        , o.`Extension`
        , o.`Zipcode`
        , o.`City`
        , o.`Country`
        , o.`BoatBrand`
        , o.`BoatType`
        , o.`Total`
        , o.`Status`
        , o.`Provision`
        , o.`CreatedAt`
        , p.`ConstructionTotal`";
    private const string modelAndSupplierJoins = "AS o INNER JOIN `floororderparts` p ON o.`Id` = p.`FloorOrderId`";

    public Task<IEnumerable<FloorOrderDto>> Handle(FloorOrderQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<FloorOrderDto>($"SELECT {modelAndSupplierColumns} FROM {DbConstants.Tables.FloorOrder} {modelAndSupplierJoins}");
    }
}
