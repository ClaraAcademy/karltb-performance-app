using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class PerformanceControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private readonly string _baseUrl = "api/performance";

    [Fact]
    public async Task GetKeyFigures_Unauthenticated_Returns_Unauthorized()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}?portfolioId=1");

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetKeyFigures_Authenticated_Returns_Ok_With_Data()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}?portfolioId=1");
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var keyFigureDtos = await response.Content.ReadFromJsonAsync<List<PortfolioBenchmarkKeyFigureDTO>>();
        Assert.NotNull(keyFigureDtos);
        Assert.NotEmpty(keyFigureDtos);
    }
}