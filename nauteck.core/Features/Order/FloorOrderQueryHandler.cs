using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Dto.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.FloorOrderQuery, IEnumerable<FloorOrderDto>>
{   

    private const string modelAndSupplierColumns = @$"
        CAST(o.{DbConstants.Columns.Id} AS NCHAR) AS {DbConstants.Columns.Id}
        , o.{DbConstants.Columns.Reference}
        , o.{DbConstants.Columns.LastName}
        , o.{DbConstants.Columns.FirstName}
        , o.{DbConstants.Columns.Preamble}
        , o.{DbConstants.Columns.Infix}
        , o.{DbConstants.Columns.Address}
        , o.{DbConstants.Columns.Number}
        , o.{DbConstants.Columns.Extension}
        , o.{DbConstants.Columns.Zipcode}
        , o.{DbConstants.Columns.City}
        , o.{DbConstants.Columns.Country}
        , o.{DbConstants.Columns.BoatBrand}
        , o.{DbConstants.Columns.BoatType}
        , o.{DbConstants.Columns.Total}
        , o.{DbConstants.Columns.Status}
        , o.{DbConstants.Columns.Provision}
        , o.{DbConstants.Columns.Discount}
        , o.{DbConstants.Columns.CreatedAt}
        , p.{DbConstants.Columns.ConstructionTotal}";
    private const string modelAndSupplierJoins = $"AS o INNER JOIN {DbConstants.Tables.FloorOrderParts} p ON o.{DbConstants.Columns.Id} = p.{DbConstants.Columns.FloorOrderId}";

    public Task<IEnumerable<FloorOrderDto>> Handle(Queries.FloorOrderQuery request, CancellationToken cancellationToken)
    {
        var query = $"SELECT {modelAndSupplierColumns} FROM {DbConstants.Tables.FloorOrder} {modelAndSupplierJoins}";
        return dapperContext.Connection.QueryAsync<FloorOrderDto>(query);
    }
}
