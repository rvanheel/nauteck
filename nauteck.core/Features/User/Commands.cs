using nauteck.data.Models.User;

namespace nauteck.core.Features.User;

public static class Commands
{
    public sealed record DeleteUserCommand(Guid Id) : IRequest;
    public sealed record SaveOrUpdateUserCommand(UserPostModel User, string UserName, Guid DealerId) : IRequest;
}
