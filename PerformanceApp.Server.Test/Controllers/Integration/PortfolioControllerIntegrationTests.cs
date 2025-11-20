using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class PortfolioControllerIntegrationTests(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private static readonly string PortfolioEndpoint = "/api/Portfolio";
    private static readonly string PortfolioBenchmarkEndpoint = "/api/PortfolioBenchmark";
    [Fact]
    public async Task GetPortfolios_Unauthorized_Returns401()
    {
        // Act
        var response = await _client.GetAsync(PortfolioEndpoint);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetPortfolios_Authorized_Returns200()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, PortfolioEndpoint);
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_Authorized_Returns200()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, PortfolioBenchmarkEndpoint);
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_WithPortfolioId_Authorized_Returns200()
    {
        // Arrange
        var portfolioId = 1;
        var request = new HttpRequestMessage(HttpMethod.Get, $"{PortfolioBenchmarkEndpoint}?portfolioId={portfolioId}");
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_Unauthorized_Returns401()
    {
        // Act
        var response = await _client.GetAsync(PortfolioBenchmarkEndpoint);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

}