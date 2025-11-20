using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class PositionControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private static readonly string _baseUrl = "api/position";

    private static string GetEndpoint(string instrumentType)
    {
        return $"{_baseUrl}/{instrumentType.ToLower()}?portfolioId=1&date=2017-03-14";
    }

    private static string GetStockEndpoint() => GetEndpoint("stocks");
    private static string GetBondEndpoint() => GetEndpoint("bonds");
    private static string GetIndexEndpoint() => GetEndpoint("indexes");

    [Fact]
    public async Task GetStockPositions_Unauthenticated_Returns_Unauthorized() => await Get_Unauthenticated_Returns_Unauthorized(GetStockEndpoint());
    [Fact]
    public async Task GetStockPositions_Authenticated_Returns_Ok_With_Data() => await Get_Authenticated_Returns_Ok(GetStockEndpoint());
    [Fact]
    public async Task GetBondPositions_Unauthenticated_Returns_Unauthorized() => await Get_Unauthenticated_Returns_Unauthorized(GetBondEndpoint());
    [Fact]
    public async Task GetBondPositions_Authenticated_Returns_Ok() => await Get_Authenticated_Returns_Ok(GetBondEndpoint());
    [Fact]
    public async Task GetIndexPositions_Unauthenticated_Returns_Unauthorized() => await Get_Unauthenticated_Returns_Unauthorized(GetIndexEndpoint());
    [Fact]
    public async Task GetIndexPositions_Authenticated_Returns_Ok() => await Get_Authenticated_Returns_Ok(GetIndexEndpoint());
}