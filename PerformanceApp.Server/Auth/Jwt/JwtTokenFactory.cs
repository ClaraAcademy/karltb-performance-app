using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Auth.Jwt.Claims;
using PerformanceApp.Server.Auth.Jwt.Credentials;
using PerformanceApp.Server.Auth.Jwt.Duration;

namespace PerformanceApp.Server.Auth.Jwt;

public class JwtTokenFactory
{
    private SigningCredentials _credentials;

    public JwtTokenFactory(SymmetricSecurityKey key)
    {
        _credentials = SigningCredentialsFactory.CreateSigningCredentials(key);
    }

    public SecurityTokenDescriptor CreateTokenDescriptor(ApplicationUser user)
    {
        var claims = ClaimFactory.CreateUserClaims(user);
        var expiryTime = DurationFactory.CreateDefaultExpiryTime();

        return CreateTokenDescriptor(claims, expiryTime);
    }

    SecurityTokenDescriptor CreateTokenDescriptor(ClaimsIdentity claimsIdentity, DateTime expiryTime)
    {
        return new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = expiryTime,
            SigningCredentials = _credentials
        };
    }

}