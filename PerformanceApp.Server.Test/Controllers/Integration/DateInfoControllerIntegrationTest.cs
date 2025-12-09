using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public class DateInfoControllerIntegrationTest(WebApplicationFactory<Program> factory, DatabaseFixture fixture) 
    : BaseControllerIntegrationTests(factory, fixture)
{
    private readonly string _baseUrl = "api/DateInfo";

    [Fact]
    public async Task GetDates_Unauthenticated_Returns_Unauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(_baseUrl);
    }

    [Fact]
    public async Task GetDates_Authenticated_Returns_Ok()
    {
        await Get_Authenticated_Returns_Ok(_baseUrl);
    }
}