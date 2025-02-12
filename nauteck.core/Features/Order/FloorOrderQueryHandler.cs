using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Dto.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderQueryHandler(IDbConnection dbConnection) : IRequestHandler<Queries.FloorOrderQuery, IEnumerable<FloorOrderDto>>
{   

    private const string modelAndSupplierColumns = @$"
        CAST(o.{DbConstants.Columns.Id} AS NCHAR) AS {DbConstants.Columns.Id}
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
        , o.{DbConstants.Columns.CreatedAt}
        , p.`ConstructionTotal`";
    private const string modelAndSupplierJoins = $"AS o INNER JOIN {DbConstants.Tables.FloorOrderParts} p ON o.{DbConstants.Columns.Id} = p.{DbConstants.Columns.FloorOrderId}";

    public Task<IEnumerable<FloorOrderDto>> Handle(Queries.FloorOrderQuery request, CancellationToken cancellationToken)
    {
        var query = $"SELECT {modelAndSupplierColumns} FROM {DbConstants.Tables.FloorOrder} {modelAndSupplierJoins}";
        return dbConnection.QueryAsync<FloorOrderDto>(query);
    }
}
