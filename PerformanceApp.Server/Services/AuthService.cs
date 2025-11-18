using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
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
    private static AuthResult GetInvalidLoginResult()
    {
        var message = "Invalid username or password";
        return new AuthResult { Success = false, ErrorMessage = message };
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == null)
        {
            return GetInvalidLoginResult();
        }

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordValid)
        {
            return GetInvalidLoginResult();
        }

        return new AuthResult { Success = true, Token = _jwtService.GenerateJwtToken(user) };
    }

    public async Task<AuthResult> LogoutAsync()
    {
        return await Task.FromResult(new AuthResult { Success = true });
    }
}
