using MediatR;

using Dapper;

using nauteck.data.Entities.Floor.Color;
using System.Data;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Floor.Color;

public sealed record FloorColorQuery : IRequest<IEnumerable<FloorColor>>;
public sealed class FloorColorQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorColorQuery, IEnumerable<FloorColor>>
{
    public Task<IEnumerable<FloorColor>> Handle(FloorColorQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<FloorColor>($"SELECT * FROM {DbConstants.Tables.FloorColor}");
    }
}
