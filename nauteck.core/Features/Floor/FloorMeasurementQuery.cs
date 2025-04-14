using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorMeasurementQuery : IRequest<IEnumerable<FloorMeasurement>>;

public sealed class FloorMeasurementQueryHandler(IDapperContext dapperContext) : IRequestHandler<FloorMeasurementQuery, IEnumerable<FloorMeasurement>>
{
    public Task<IEnumerable<FloorMeasurement>> Handle(FloorMeasurementQuery request, CancellationToken cancellationToken)
    {
        return dapperContext.Connection.QueryAsync<FloorMeasurement>($"SELECT * FROM {DbConstants.Tables.FloorMeasurement} ORDER BY `Description`");
    }
}