namespace nauteck.core.Features.Status;

public static class Queries
{
    public sealed record StatusByIdQuery(Guid Id) : IRequest<data.Entities.Status.Status>;
    public sealed record StatusQuery: IRequest<IEnumerable<data.Entities.Status.Status>>;
}
