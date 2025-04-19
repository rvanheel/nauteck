using System.Security.Claims;
using nauteck.data.Enums;
using nauteck.core.Implementation;
using static nauteck.core.Features.Account.Queries;

namespace nauteck.core.Features.Account.Handlers.Query;


public sealed class SignInQueryHandler(IDapperContext dapperContext, IHelper helper) : IRequestHandler<SignInQuery, ClaimsPrincipal?>
{
    public async Task<ClaimsPrincipal?> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
        // find the user
        var u = await GetUserByEmail(request.UserName);

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

        claims.AddRange(Enum.GetValues<RoleType>()
                .Cast<RoleType>()
                .Where(x => u.Roles.HasFlag(x))
                .Select(x => new Claim(ClaimTypes.Role, Enum.GetName(x) ?? string.Empty)));

        return [.. claims];
    }
    private Task<data.Entities.Account.User?> GetUserByEmail(string? email)
    {
        const string query = $"SELECT * FROM {DbConstants.Tables.User} WHERE Email = @Email AND Active = 1 LIMIT 1";
        return dapperContext.Connection.QueryFirstOrDefaultAsync<data.Entities.Account.User>(query, new { email });
    }
    #endregion
}
