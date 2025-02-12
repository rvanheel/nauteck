using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorLogoQuery : IRequest<IEnumerable<FloorLogo>>;

public sealed class FloorOrderLogoQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorLogoQuery, IEnumerable<FloorLogo>>
{
    public Task<IEnumerable<FloorLogo>> Handle(FloorLogoQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<FloorLogo>($"SELECT * FROM {DbConstants.Tables.FloorLogo} ORDER BY `Description`");
    }
}
