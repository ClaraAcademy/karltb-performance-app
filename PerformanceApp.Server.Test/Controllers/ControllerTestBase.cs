using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PerformanceApp.Server.Test.Controllers;

public class ControllerTestBase
{
    protected static Claim[] CreateUserClaims(string username)
    {
        return [new Claim(ClaimTypes.Name, username)];
    }
    protected static ClaimsIdentity CreateUserIdentity(string username)
    {
        return new ClaimsIdentity(CreateUserClaims(username), "mock");
    }
    protected static ClaimsPrincipal CreateUserPrincipal(string username)
    {
        return new ClaimsPrincipal(CreateUserIdentity(username));
    }

    protected static DefaultHttpContext CreateHttpContext(ClaimsPrincipal user)
    {
        return new DefaultHttpContext { User = user };
    }
    protected static ControllerContext CreateControllerContext(ClaimsPrincipal user)
    {
        return new ControllerContext { HttpContext = CreateHttpContext(user) };
    }
}