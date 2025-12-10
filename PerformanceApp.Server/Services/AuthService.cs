using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Auth.Result;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Services;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password);
    Task<AuthResult> LogoutAsync();
}

public class AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return AuthResultFactory.DefaultInvalidLoginResult;
        }

        var validPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!validPassword)
        {
            return AuthResultFactory.DefaultInvalidLoginResult;
        }

        var token = _jwtService.GenerateJwtToken(user);
        return AuthResultFactory.CreateSuccessResult(token);
    }

    public Task<AuthResult> LogoutAsync()
    {
        return Task.FromResult(AuthResultFactory.DefaultSuccessResult);
    }
}
