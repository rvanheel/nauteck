using MediatR;

using Microsoft.EntityFrameworkCore;

using nauteck.data.Entities.Floor.Color;
using nauteck.persistence;

namespace nauteck.core.Features.Floor.Color;

public sealed record FloorColorExclusiveQuery: IRequest<FloorColorExclusive[]>;

public sealed class FloorColorExclusiveQueryHandler(AppDbContext appDbContext) : IRequestHandler<FloorColorExclusiveQuery, FloorColorExclusive[]>
{
    public Task<FloorColorExclusive[]> Handle(FloorColorExclusiveQuery request, CancellationToken cancellationToken)
    {
        return appDbContext.FloorColorExclusive
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }
}