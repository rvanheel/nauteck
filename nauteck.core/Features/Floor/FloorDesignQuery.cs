using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorDesignQuery : IRequest<IEnumerable<FloorDesign>>;

public sealed class FloorDesignQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorDesignQuery, IEnumerable<FloorDesign>>
{
    public Task<IEnumerable<FloorDesign>> Handle(FloorDesignQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<FloorDesign>($"SELECT * FROM {DbConstants.Tables.FloorDesign}");
    }
}