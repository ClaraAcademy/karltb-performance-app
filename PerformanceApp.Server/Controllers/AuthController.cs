using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;
namespace PerformanceApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService service) : MyControllerBase
{
    private readonly IAuthService _service = service;
    private readonly string LogoutErrorMessage = "Logout failed";

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _service.LoginAsync(request.Username, request.Password);

        if (!result.Success)
        {
            return UnauthorizedResponse();
        }

        var token = result.Token;

        if (token == null)
        {
            return UnauthorizedResponse();
        }

        return Ok(new LoginResponse(token));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var result = await _service.LogoutAsync();

        if (!result.Success)
        {
            return BadRequestResponse(result.ErrorMessage ?? LogoutErrorMessage);
        }

        return Ok();
    }

}
