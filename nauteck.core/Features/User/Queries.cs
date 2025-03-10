using MediatR;

using nauteck.data.Dto.Account;
using nauteck.data.Models.User;

namespace nauteck.core.Features.User;

public static class Queries
{
    public sealed record UserByIdQuery(Guid Id, Guid DealerId) : IRequest<UserPostModel>;
    public sealed record UserQuery : IRequest<IEnumerable<AccountDto>>;
}
