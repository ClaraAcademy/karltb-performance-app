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

    /// <summary>
    /// Logs in a user with the provided credentials.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A newly created LoginResponse</returns>
    /// <remarks>
    /// Sample request:
    ///     
    ///     POST /api/auth/login
    ///     {
    ///        "username": "user1",
    ///        "password": "SuperSecretPassword123"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Login successful</response>
    /// <response code="401">Invalid username or password</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    /// <returns>Nothing</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/auth/logout
    ///     Authorization: Bearer {token}
    /// 
    /// </remarks>
    /// <response code="200">Logout successful</response>
    /// <response code="400">Logout failed</response>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
