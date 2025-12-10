using PerformanceApp.Server.Auth.Constants;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Auth.Result.Defaults;

public static class AuthResultDefaults
{
    public static AuthResult DefaultInvalidLoginResult => _defaultInvalidLoginResult;
    public static AuthResult DefaultSuccessResult => _defaultSuccessResult;

    private static readonly AuthResult _defaultInvalidLoginResult = new AuthResult
    {
        Success = false,
        Token = null,
        ErrorMessage = AuthConstants.InvalidLoginMessage
    };

    private static readonly AuthResult _defaultSuccessResult = new AuthResult
    {
        Success = true,
        Token = null,
        ErrorMessage = null
    };
}