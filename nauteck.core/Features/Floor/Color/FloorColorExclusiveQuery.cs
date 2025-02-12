using MediatR;

using Dapper;

using nauteck.data.Entities.Floor.Color;
using System.Data;
using nauteck.core.Implementation;
namespace nauteck.core.Features.Floor.Color;

public sealed record FloorColorExclusiveQuery: IRequest<IEnumerable<FloorColorExclusive>>;

public sealed class FloorColorExclusiveQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorColorExclusiveQuery, IEnumerable<FloorColorExclusive>>
{
    public Task<IEnumerable<FloorColorExclusive>> Handle(FloorColorExclusiveQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<FloorColorExclusive>($"SELECT * FROM {DbConstants.Tables.FloorColor} ORDER BY `Description`");
    }
}