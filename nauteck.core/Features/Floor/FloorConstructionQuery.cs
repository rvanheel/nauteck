using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorConstructionQuery : IRequest<IEnumerable<FloorConstruction>>;

public sealed class FloorConstructionQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorConstructionQuery, IEnumerable<FloorConstruction>>
{
    public Task<IEnumerable<FloorConstruction>> Handle(FloorConstructionQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<FloorConstruction>($"SELECT * FROM {DbConstants.Tables.FloorConstruction} ORDER BY `Description`");
    }
}