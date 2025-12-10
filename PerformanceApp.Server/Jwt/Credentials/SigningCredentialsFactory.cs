using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Server.Jwt.Credentials.Constants;

namespace PerformanceApp.Server.Jwt.Credentials;

public static class SigningCredentialsFactory
{
    public static SigningCredentials CreateSigningCredentials(SymmetricSecurityKey key, string algorithm)
    {
        return new SigningCredentials(key, algorithm);
    }

    public static SigningCredentials CreateSigningCredentials(SymmetricSecurityKey key)
    {
        var algorithm = SigningCredentialsConstants.DefaultAlgorithm;
        return CreateSigningCredentials(key, algorithm);
    }
}