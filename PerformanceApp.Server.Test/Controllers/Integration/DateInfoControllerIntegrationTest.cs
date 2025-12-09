using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class DateInfoControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private readonly string _baseUrl = "api/DateInfo";

    [Fact]
    public async Task GetDates_Unauthenticated_Returns_Unauthorized() => await Get_Unauthenticated_Returns_Unauthorized(_baseUrl);

    [Fact]
    public async Task GetDates_Authenticated_Returns_Ok() => await Get_Authenticated_Returns_Ok(_baseUrl);
}