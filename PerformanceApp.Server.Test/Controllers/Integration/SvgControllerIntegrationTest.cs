using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class SvgControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private static string GetEndpoint() => $"api/svg?portfolioId={1}&width={100}&height={100}";

    [Fact]
    public async Task GetCumulativePerformanceGraph_Unauthenticated_Returns_Unauthorized() => await Get_Unauthenticated_Returns_Unauthorized(GetEndpoint());

    [Fact]
    public async Task GetCumulativePerformanceGraph_Authenticated_Returns_Ok() => await Get_Authenticated_Returns_Ok(GetEndpoint());
}