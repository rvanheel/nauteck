using System.Data;

using Dapper;

using MediatR;

using nauteck.core.Implementation;
using nauteck.data.Entities.Floor;

namespace nauteck.core.Features.Floor;

public sealed record FloorMeasurementQuery : IRequest<IEnumerable<FloorMeasurement>>;

public sealed class FloorMeasurementQueryHandler(IDbConnection dbConnection) : IRequestHandler<FloorMeasurementQuery, IEnumerable<FloorMeasurement>>
{
    public Task<IEnumerable<FloorMeasurement>> Handle(FloorMeasurementQuery request, CancellationToken cancellationToken)
    {
        return dbConnection.QueryAsync<FloorMeasurement>($"SELECT * FROM {DbConstants.Tables.FloorMeasurement} ORDER BY `Description`");
    }
}