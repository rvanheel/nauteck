using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;

namespace nauteck.core.Features.Floor;

public sealed record FloorQuery : IRequest<IEnumerable<data.Entities.Floor.Floor>>;

public sealed class FloorQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorQuery, IEnumerable<data.Entities.Floor.Floor>>
{
    public Task<IEnumerable<data.Entities.Floor.Floor>> Handle(FloorQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<data.Entities.Floor.Floor>($"SELECT * FROM {DbConstants.Tables.Floor}");
    }
}
