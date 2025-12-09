using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Controllers;

public class MyControllerBase : ControllerBase
{
    protected const string AuthenticationErrorMessage = "Authentication Failed";

    protected bool UserIsAuthenticated()
    {
        return User?.Identity?.IsAuthenticated ?? false;
    }

    protected UnauthorizedObjectResult UnauthorizedResponse(string errorMessage)
    {
        return Unauthorized(new ErrorResponse(errorMessage));
    }

    protected UnauthorizedObjectResult UnauthorizedResponse()
    {
        return UnauthorizedResponse(AuthenticationErrorMessage);
    }

    protected BadRequestObjectResult BadRequestResponse(string errorMessage)
    {
        return BadRequest(new ErrorResponse(errorMessage));
    }
}