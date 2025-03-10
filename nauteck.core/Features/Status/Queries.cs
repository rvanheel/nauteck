using MediatR;

namespace nauteck.core.Features.Status;

public static class Queries
{
    public sealed class  StatusQuery: IRequest<IEnumerable<data.Entities.Status.Status>>;
}
