using Dapper;

using MediatR;

using nauteck.core.Abstraction;
using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorDesignQuery : IRequest<IEnumerable<FloorDesign>>;

public sealed class FloorDesignQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorDesignQuery, IEnumerable<FloorDesign>>
{
    public Task<IEnumerable<FloorDesign>> Handle(FloorDesignQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<FloorDesign>($"SELECT * FROM {DbConstants.Tables.FloorDesign} ORDER BY `Description`");
    }
}