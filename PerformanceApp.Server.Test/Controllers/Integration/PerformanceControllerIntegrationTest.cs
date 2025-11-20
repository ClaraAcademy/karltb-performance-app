using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class PerformanceControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private readonly string Endpoint = "api/performance?portfolioId=1";

    [Fact]
    public async Task GetKeyFigures_Unauthenticated_Returns_Unauthorized() => await Get_Unauthenticated_Returns_Unauthorized(Endpoint);
    [Fact]
    public async Task GetKeyFigures_Authenticated_Returns_Ok() => await Get_Authenticated_Returns_Ok(Endpoint);
}