namespace nauteck.core.Features.Client;

public static class Queries
{
    public sealed record ClientQuery : IRequest<IEnumerable<data.Dto.Client.ClientDto>>;
    public sealed record ClientByIdQuery(Guid Id) : IRequest<data.Entities.Client.Client>;
}
