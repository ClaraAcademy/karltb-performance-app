using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public class PortfolioControllerIntegrationTests(WebApplicationFactory<Program> factory, DatabaseFixture fixture) 
    : BaseControllerIntegrationTests(factory, fixture)
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