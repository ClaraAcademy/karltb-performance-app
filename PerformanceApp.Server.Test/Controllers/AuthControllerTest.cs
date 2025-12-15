using Moq;
using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using PerformanceApp.Data.Dtos;
namespace PerformanceApp.Server.Test.Controllers;

public class AuthControllerTest : ControllerTestBase
{
    private readonly Mock<IAuthService> _mockAuthService;
    public AuthControllerTest()
    {
        _mockAuthService = new Mock<IAuthService>();
    }


    [Fact]
    public async Task Login_ReturnsOk_WhenCredentialsAreValid()
    {
        // Arrange
        var username = "user";
        var password = "pass";
        _mockAuthService.Setup(s => s.LoginAsync(username, password))
            .ReturnsAsync(new AuthResult { Success = true, Token = "jwt-token" });

        var controller = new AuthController(_mockAuthService.Object);

        // Act
        var loginRequest = new LoginRequest { Username = username, Password = password };
        var result = await controller.Login(loginRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.NotNull(okResult.Value);
        var loginResponse = Assert.IsType<LoginResponse>(okResult.Value);
        Assert.Equal("jwt-token", loginResponse.Token);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var loginRequest = new LoginRequest { Username = "user", Password = "wrongpass" };
        _mockAuthService.Setup(s => s.LoginAsync("user", "wrongpass"))
            .ReturnsAsync(new AuthResult { Success = false, ErrorMessage = "Invalid credentials" });

        var controller = new AuthController(_mockAuthService.Object);

        // Act
        var result = await controller.Login(loginRequest);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal(401, unauthorizedResult.StatusCode);
        var errorResponse = Assert.IsType<ErrorResponse>(unauthorizedResult.Value);
    }

    [Fact]
    public async Task Logout_ReturnsOk_WhenLogoutSucceeds()
    {
        // Arrange
        _mockAuthService.Setup(s => s.LogoutAsync())
            .ReturnsAsync(new AuthResult { Success = true });

        var controller = new AuthController(_mockAuthService.Object);
        var user = CreateUserPrincipal("testuser");
        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.Logout();

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Logout_ReturnsBadRequest_WhenLogoutFails()
    {
        // Arrange
        _mockAuthService.Setup(s => s.LogoutAsync())
            .ReturnsAsync(new AuthResult { Success = false, ErrorMessage = "Logout failed" });

        var controller = new AuthController(_mockAuthService.Object);
        var user = CreateUserPrincipal("testuser");
        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.Logout();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.NotNull(badRequestResult.Value);
        var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);
        Assert.Equal("Logout failed", errorResponse.Message);
    }
}