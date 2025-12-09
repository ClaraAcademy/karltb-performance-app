using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public class SvgControllerIntegrationTest(WebApplicationFactory<Program> factory, DatabaseFixture fixture) 
    : BaseControllerIntegrationTests(factory, fixture)
{
    private static string Endpoint => $"api/svg?portfolioId={1}&width={100}&height={100}";

    [Fact]
    public async Task GetCumulativePerformanceGraph_Unauthenticated_Returns_Unauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(Endpoint);
    }

    [Fact]
    public async Task GetCumulativePerformanceGraph_Authenticated_Returns_Ok()
    {
        await Get_Authenticated_Returns_Ok(Endpoint);
    }
}