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
    public async Task GetPortfolios_Unauthenticated_ReturnsUnauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(PortfolioEndpoint);
    }

    [Fact]
    public async Task GetPortfolios_Authenticated_ReturnsOk()
    {
        await Get_Authenticated_Returns_Ok(PortfolioEndpoint);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_Authorized_Returns200()
    {
        await Get_Authenticated_Returns_Ok(PortfolioBenchmarkEndpoint);
    }

    [Fact]
    public async Task GetPortfolioBenchmarks_Unauthorized_Returns401()
    {
        await Get_Unauthenticated_Returns_Unauthorized(PortfolioBenchmarkEndpoint);
    }

}