using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;



namespace PerformanceApp.Server.Test.Controllers.Integration;

public class BaseControllerIntegrationTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly HttpClient _client = factory.CreateClient();
    protected readonly IJwtService _jwtService = GetJwtService(factory);
    protected readonly ApplicationUser TestUser = new ApplicationUser { UserName = "IntegrationTestUser" };

    protected static IJwtService GetJwtService(WebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IJwtService>();
    }

    protected void AddAuthorizationHeader(HttpRequestMessage request, ApplicationUser user)
    {
        var token = _jwtService.GenerateJwtToken(user);
        request.Headers.Add("Authorization", $"Bearer {token}");
    }
}