using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Order;

namespace nauteck.core.Features.Order;

public sealed record FloorOrderQuery : IRequest<IEnumerable<FloorOrder>>;

public sealed class FloorOrderQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorOrderQuery, IEnumerable<FloorOrder>>
{
    public Task<IEnumerable<FloorOrder>> Handle(FloorOrderQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"SELECT CAST(`Id` AS NCHAR) AS `Id`,
            `Preamble`,`LastName`,`FirstName`,`Infix`,
            `Address`,`Number`, `Extension`, `Zipcode`, `City`, `Region`, `Country`,
            `BoatBrand`,`BoatType`,
            `Provision`,`Reference`,`Status`,`Total`
            FROM {DbConstants.Tables.FloorOrder}";
        return dbConnection.QueryAsync<FloorOrder>(sql);
    }
}
