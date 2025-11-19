using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Services;

public interface IJwtService
{
    string GenerateJwtToken(ApplicationUser user);
    bool ValidateJwtToken(string token);
}

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly byte[] _key = System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!);

    private static List<Claim> GetUserClaims(ApplicationUser user)
    {
        return [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
        ];
    }

    private static SecurityTokenDescriptor GetTokenDescriptor(ApplicationUser user, byte[] key)
    {
        var claims = GetUserClaims(user);

        return new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
    }

    public string GenerateJwtToken(ApplicationUser user)
    {
        var tokenDescriptor = GetTokenDescriptor(user, _key);

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    private static TokenValidationParameters GetTokenValidationParameters(byte[] key)
    {
        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    }


    public bool ValidateJwtToken(string token)
    {
        try
        {
            _tokenHandler.ValidateToken(
                token,
                GetTokenValidationParameters(_key),
                out SecurityToken _
            );

            return true;
        }
        catch
        {
            return false;
        }
    }

}