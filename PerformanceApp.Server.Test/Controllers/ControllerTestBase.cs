using System.Security.Claims;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceApp.Data.Seeding.Dtos;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;


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

    protected static void AssertIsOk<T>(ActionResult<IEnumerable<T>> result, int expected)
    {
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<T>>(okResult.Value);
        Assert.Equal(expected, returnValue.Count());
    }

    protected static void AssertIsNotFound<T>(ActionResult<IEnumerable<T>> result)
    {
        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        Assert.Equal(StatusCodes.NotFound, notFoundResult.StatusCode);
    }

    protected static void AssertIsUnauthorized<T>(ActionResult<IEnumerable<T>> result)
    {
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Unauthorized, unauthorizedResult.StatusCode);
    }
}