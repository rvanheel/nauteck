using MediatR;

using Dapper;

using nauteck.data.Entities.Floor.Color;
using nauteck.core.Implementation;
using nauteck.core.Abstraction;

namespace nauteck.core.Features.Floor.Color;

public sealed record FloorColorQuery : IRequest<IEnumerable<FloorColor>>;
public sealed class FloorColorQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorColorQuery, IEnumerable<FloorColor>>
{
    public Task<IEnumerable<FloorColor>> Handle(FloorColorQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<FloorColor>($"SELECT * FROM {DbConstants.Tables.FloorColor} ORDER BY `Description`");
    }
}
