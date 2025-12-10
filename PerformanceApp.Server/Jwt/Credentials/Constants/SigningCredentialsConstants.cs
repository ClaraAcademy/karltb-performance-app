using Microsoft.IdentityModel.Tokens;

namespace PerformanceApp.Server.Jwt.Credentials.Constants;

public static class SigningCredentialsConstants
{
    private const string _defaultAlgorithm = SecurityAlgorithms.HmacSha256Signature;
    public static string DefaultAlgorithm => _defaultAlgorithm;
}