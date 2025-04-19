using Microsoft.Extensions.Caching.Memory;

using nauteck.core.Implementation;

using static nauteck.core.Features.Queries.Floor;

namespace nauteck.core.Features.Handlers.Query.Floor;

public sealed class FloorQueryHandler(IDapperContext dapperContext, IMemoryCache memoryCache) : IRequestHandler<FloorQuery, IEnumerable<data.Entities.Floor.Floor>?>
{
    public Task<IEnumerable<data.Entities.Floor.Floor>?> Handle(FloorQuery request, CancellationToken cancellationToken)
    {
        return memoryCache.GetOrCreateAsync(nameof(FloorQuery), entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return dapperContext.Connection.QueryAsync<data.Entities.Floor.Floor>($"SELECT * FROM {DbConstants.Tables.Floor.TableName} ORDER BY {DbConstants.Tables.Floor.Columns.Description} ASC");
        });
        
    }
}