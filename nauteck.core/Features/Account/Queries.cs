using System.Security.Claims;

namespace nauteck.core.Features.Account;

public static class Queries
{    
    public sealed record SignInQuery(string? AuthenticationScheme, string? Password, string? UserName) : IRequest<ClaimsPrincipal?>;
}
