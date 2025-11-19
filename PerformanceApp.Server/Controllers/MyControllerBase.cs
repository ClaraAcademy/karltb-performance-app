using Microsoft.AspNetCore.Mvc;

namespace PerformanceApp.Server.Controllers;

public class MyControllerBase : ControllerBase
{
    protected const string AuthenticationErrorMessage = "Authentication Failed";

    protected bool UserIsAuthenticated()
    {
        return User?.Identity?.IsAuthenticated ?? false;
    }
}