using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public class AuthControllerIntegrationTest(WebApplicationFactory<Program> factory, DatabaseFixture fixture)
    : BaseControllerIntegrationTests(factory, fixture)
{
    private static readonly string LoginEndpoint = "/api/Auth/login";
    private static readonly string LogoutEndpoint = "/api/Auth/logout";

    private static LoginRequest GetLoginRequest(string username, string password)
    {
        return new LoginRequest
        {
            Username = username,
            Password = password
        };
    }
    private static LoginRequest GetValidLoginRequest()
    {
        return GetLoginRequest(UserData.UsernameA, UserData.Password);
    }
    private static LoginRequest GetInvalidLoginRequest()
    {
        return GetLoginRequest("invaliduser", "invalidpassword");
    }

    [Fact]
    public async Task Login_ValidCredentials_Returns200AndToken()
    {
        // Arrange
        var loginRequest = GetValidLoginRequest();

        var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginRequest), System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(LoginEndpoint, content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = await response.Content.ReadAsStringAsync();
        var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(responseContent, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrEmpty(loginResponse.Token));
    }

    [Fact]
    public async Task Login_InvalidCredentials_Returns401()
    {
        // Arrange
        var loginRequest = GetInvalidLoginRequest();

        var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(loginRequest), System.Text.Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(LoginEndpoint, content);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Logout_Authorized_ReturnsOk()
    {
        await Post_Authenticated_Returns_Ok(LogoutEndpoint);
    }

    [Fact]
    public async Task Logout_Unauthorized_Returns401()
    {
        await Post_Unauthenticated_Returns_Unauthorized(LogoutEndpoint);
    }

}