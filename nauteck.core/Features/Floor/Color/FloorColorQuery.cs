using MediatR;

using Microsoft.EntityFrameworkCore;

using nauteck.data.Entities.Floor.Color;
using nauteck.persistence;

namespace nauteck.core.Features.Floor.Color;

public sealed record FloorColorQuery : IRequest<FloorColor[]>;
public sealed class FloorColorQueryHandler(AppDbContext appDbContext) : IRequestHandler<FloorColorQuery, FloorColor[]>
{
    public Task<FloorColor[]> Handle(FloorColorQuery request, CancellationToken cancellationToken)
    {
        return appDbContext.FloorColor
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);
    }
}
