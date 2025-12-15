using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Server.Services;
using System.Net;


namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public abstract class BaseControllerIntegrationTests(WebApplicationFactory<Program> factory, DatabaseFixture fixture) 
{
    protected readonly HttpClient _client = factory.CreateClient();
    protected readonly IJwtService _jwtService = GetJwtService(factory);
    protected readonly ApplicationUser TestUser = GetTestUser(factory);

    private readonly DatabaseFixture _fixture = fixture;

    protected static IJwtService GetJwtService(WebApplicationFactory<Program> factory)
    {
        using var scope = factory
            .Services
            .CreateScope();
        return scope
            .ServiceProvider
            .GetRequiredService<IJwtService>();
    }

    private static ApplicationUser GetTestUser(WebApplicationFactory<Program> factory)
    {
        using var scope = factory
            .Services
            .CreateScope();
        var userManager = scope
            .ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        var user = userManager
            .FindByNameAsync(UserData.UsernameB)
            .GetAwaiter()
            .GetResult();
        return user!;
    }

    private static void AddAuthorizationHeader(HttpRequestMessage request, string token)
    {
        request.Headers.Add("Authorization", $"Bearer {token}");
    }
    protected void AddAuthorizationHeader(HttpRequestMessage request, ApplicationUser user)
    {
        var token = _jwtService.GenerateJwtToken(user);
        AddAuthorizationHeader(request, token);
    }
    protected void AddAuthorizationHeader(HttpRequestMessage request, LoginResponse loginResult)
    {
        AddAuthorizationHeader(request, loginResult.Token);
    }

    protected async Task Post_Unauthenticated_Returns_Unauthorized(string url)
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, url);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    protected async Task Post_Authenticated_Returns_Ok(string url)
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    protected async Task Get_Unauthenticated_Returns_Unauthorized(string url)
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    protected async Task Get_Authenticated_Returns_Ok(string url)
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}