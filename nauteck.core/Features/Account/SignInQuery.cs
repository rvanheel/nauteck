using System.Security.Claims;

using MediatR;

using Microsoft.EntityFrameworkCore;

using nauteck.core.Abstraction;
using nauteck.data.Enums;
using nauteck.persistence;

namespace nauteck.core.Features.Account;

public sealed record SignInQuery(string? AuthenticationScheme, string? Password, string? UserName) : IRequest<ClaimsPrincipal?>;


public sealed class SignInQueryHandler(AppDbContext appDbContext, IHelper helper) : IRequestHandler<SignInQuery, ClaimsPrincipal?>
{
    public async Task<ClaimsPrincipal?> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        // find the user
        var u = await GetUserByEmail(request.UserName, cancellationToken);

        // verify the user
        if (u is null || !helper.VerifyPassword(request.Password, u.Password)) return default;

        // create the claims
        var claims = CreateClaims(u);

        // create the principal
        var principal = new ClaimsIdentity(claims, request.AuthenticationScheme);
        return new ClaimsPrincipal(principal);
    }

    #region Private Methods
    
    private Claim[] CreateClaims(data.Entities.Account.User u)
    {
        var claims = new List<Claim>()
        {
            new("iat", DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
            new(ClaimTypes.Email, $"{u.Email}"),
            new(ClaimTypes.Name, helper.GetFullName(u.FirstName, u.Infix, u.LastName)),
            new(ClaimTypes.Sid, $"{u.Id}")
        };

        claims.AddRange(Enum.GetValues(typeof(RoleType))
                .Cast<RoleType>()
                .Where(x => u.Roles.HasFlag(x))
                .Select(x => new Claim(ClaimTypes.Role, Enum.GetName(typeof(RoleType), x) ?? string.Empty)));

        return [.. claims];
    }
    private Task<data.Entities.Account.User?> GetUserByEmail(string? email, CancellationToken cancellationToken)
    {
        return appDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email && u.Active == true, cancellationToken);
    }
    #endregion
}