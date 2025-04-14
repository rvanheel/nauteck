using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorLogoQuery : IRequest<IEnumerable<FloorLogo>>;

public sealed class FloorOrderLogoQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorLogoQuery, IEnumerable<FloorLogo>>
{
    public Task<IEnumerable<FloorLogo>> Handle(FloorLogoQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<FloorLogo>($"SELECT * FROM {DbConstants.Tables.FloorLogo} ORDER BY `Description`");
    }
}
