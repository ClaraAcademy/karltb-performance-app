using System.Security.Claims;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Jwt.Claims;

public static class ClaimFactory
{
    public static ClaimsIdentity CreateUserClaims(ApplicationUser user)
    {
        var identifier = GetIdentifierClaim(user);
        var name = GetNameClaim(user);

        return new ClaimsIdentity([identifier, name]);
    }

    static Claim GetIdentifierClaim(ApplicationUser user)
    {
        var userId = user.Id;

        return CreateClaim(ClaimTypes.NameIdentifier, userId);
    }

    static Claim GetNameClaim(ApplicationUser user)
    {
        var username = user.UserName ?? string.Empty;

        return CreateClaim(ClaimTypes.Name, username);
    }

    static Claim CreateClaim(string type, string value) => new(type, value);
}