using nauteck.data.Entities.Floor.Color;
using nauteck.core.Implementation;

namespace nauteck.core.Features.Floor.Color;

public sealed record FloorColorExclusiveQuery: IRequest<IEnumerable<FloorColorExclusive>>;

public sealed class FloorColorExclusiveQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorColorExclusiveQuery, IEnumerable<FloorColorExclusive>>
{
    public Task<IEnumerable<FloorColorExclusive>> Handle(FloorColorExclusiveQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<FloorColorExclusive>($"SELECT * FROM {DbConstants.Tables.FloorColor} ORDER BY `Description`");
    }
}