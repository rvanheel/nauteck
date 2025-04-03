using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed class FloorOrderByIdQueryHandler(IDapperContext dapperContext) : IRequestHandler<Queries.FloorOrderByIdQuery, FloorOrder>
{
    private readonly string Columns = @$"
        CAST({DbConstants.Columns.Id} AS NCHAR) AS {DbConstants.Columns.Id}
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
        ,{DbConstants.Columns.CreatedAt}
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
	
    public Task<FloorOrder> Handle(Queries.FloorOrderByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty.ToString()) return Task.FromResult(new FloorOrder { Id = Guid.Empty.ToString() });
        var query = $"SELECT {Columns} FROM {DbConstants.Tables.FloorOrder.TableName} WHERE {DbConstants.Columns.Id} = @Id";
        return dapperContext.Connection.QueryFirstAsync<FloorOrder>(query, new { request.Id });
    }
}