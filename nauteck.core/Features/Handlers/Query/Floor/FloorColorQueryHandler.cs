using nauteck.data.Entities.Floor.Color;
using nauteck.core.Implementation;
using static nauteck.core.Features.Queries.Floor;
using Microsoft.Extensions.Caching.Memory;

namespace nauteck.core.Features.Handlers.Query.Floor;

public sealed class FloorColorQueryHandler(IDapperContext dapperContext, IMemoryCache memoryCache) : IRequestHandler<FloorColorQuery, IEnumerable<FloorColor>?>
{
    public Task<IEnumerable<FloorColor>?> Handle(FloorColorQuery request, CancellationToken cancellationToken)
    {
        return memoryCache.GetOrCreateAsync(nameof(FloorColorQuery), entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return dapperContext.Connection.QueryAsync<FloorColor>($"SELECT * FROM {DbConstants.Tables.FloorColor.TableName} ORDER BY {DbConstants.Tables.FloorColor.Columns.Description}");
        });
        
    }
}
