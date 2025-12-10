using PerformanceApp.Server.Auth.Result.Defaults;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Auth.Result;

public static class AuthResultFactory
{
    public static AuthResult DefaultInvalidLoginResult => AuthResultDefaults.DefaultInvalidLoginResult;
    public static AuthResult DefaultSuccessResult => AuthResultDefaults.DefaultSuccessResult;

    public static AuthResult CreateSuccessResult(string token)
    {
        return new AuthResult { Success = true, Token = token };
    }
}