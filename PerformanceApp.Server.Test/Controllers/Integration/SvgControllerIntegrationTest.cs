using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class SvgControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private static readonly string _baseUrl = "api/svg";

    private static string GetEndpoint() => $"{_baseUrl}?portfolioId={1}&width={100}&height={100}";

    [Fact]
    public async Task GetCumulativePerformanceGraph_Unauthenticated_Returns_Unauthorized()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, GetEndpoint());

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetCumulativePerformanceGraph_Authenticated_Returns_Ok()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, GetEndpoint());
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}