using nauteck.data.Entities.Floor.Color;
using nauteck.core.Implementation;
using Microsoft.Extensions.Caching.Memory;
using static nauteck.core.Features.Queries.Floor;

namespace nauteck.core.Features.Handlers.Query.Floor;



public sealed class FloorColorExclusiveQueryHandler(IDapperContext dapperContext, IMemoryCache memoryCache) : IRequestHandler<FloorColorExclusiveQuery, IEnumerable<FloorColorExclusive>?>
{
    public Task<IEnumerable<FloorColorExclusive>?> Handle(FloorColorExclusiveQuery request, CancellationToken cancellationToken)
    {
        return memoryCache.GetOrCreateAsync(nameof(FloorColorExclusive), entry =>
        {
            entry.SetSlidingExpiration(TimeSpan.FromMinutes(10));
            return dapperContext.Connection.QueryAsync<FloorColorExclusive>($"SELECT * FROM {DbConstants.Tables.FloorColorExclusive.TableName} ORDER BY {DbConstants.Tables.FloorColorExclusive.Columns.Description}");
        });        
    }
}