using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public class PerformanceControllerIntegrationTest(WebApplicationFactory<Program> factory, DatabaseFixture fixture) 
    : BaseControllerIntegrationTests(factory, fixture)
{
    private readonly string Endpoint = "api/performance?portfolioId=3";

    [Fact]
    public async Task GetKeyFigures_Unauthenticated_Returns_Unauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(Endpoint);
    }

    [Fact]
    public async Task GetKeyFigures_Authenticated_Returns_Ok()
    {
        await Get_Authenticated_Returns_Ok(Endpoint);
    }
}