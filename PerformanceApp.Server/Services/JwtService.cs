using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Auth.Jwt;
using PerformanceApp.Server.Auth.Jwt.Keys;
using PerformanceApp.Server.Auth.Jwt.Keys.Constants;
using PerformanceApp.Server.Auth.Jwt.Validation;

namespace PerformanceApp.Server.Services;

public interface IJwtService
{
    string GenerateJwtToken(ApplicationUser user);
    bool ValidateJwtToken(string token);
}

public class JwtService : IJwtService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SymmetricSecurityKey _key;
    private readonly JwtTokenFactory _tokenFactory;

    public JwtService(IConfiguration configuration)
    {
        _tokenHandler = new JwtSecurityTokenHandler();
        var dictionaryKey = SymmetricSecurityKeyConstants.DefaultDictionaryKey;
        var keyString = configuration[dictionaryKey]!;
        _key = SymmetricSecurityKeyFactory.CreateSymmetricSecurityKey(keyString);
        _tokenFactory = new JwtTokenFactory(_key);
    }

    public string GenerateJwtToken(ApplicationUser user)
    {
        var descriptor = _tokenFactory.CreateTokenDescriptor(user);
        var token = _tokenHandler.CreateToken(descriptor);

        return _tokenHandler.WriteToken(token);
    }

    public bool ValidateJwtToken(string token)
    {
        try
        {
            var parameters = TokenValidationParametersFactory.CreateTokenValidationParameters(_key);

            _tokenHandler.ValidateToken(
                token,
                parameters,
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