using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[Collection(IntegrationCollection.Name)]
public class PositionControllerIntegrationTest(WebApplicationFactory<Program> factory, DatabaseFixture fixture)
    : BaseControllerIntegrationTests(factory, fixture)
{
    private static readonly string _baseUrl = "api/position";

    private static string GetEndpoint(string instrumentType)
    {
        return $"{_baseUrl}/{instrumentType.ToLower()}?portfolioId=1&date=2017-03-14";
    }

    private static string StockEndpoint => GetEndpoint("stocks");
    private static string BondEndpoint => GetEndpoint("bonds");
    private static string IndexEndpoint => GetEndpoint("indexes");

    [Fact]
    public async Task GetStockPositions_Unauthenticated_Returns_Unauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(StockEndpoint);
    }

    [Fact]
    public async Task GetStockPositions_Authenticated_Returns_Ok_With_Data()
    {
        await Get_Authenticated_Returns_Ok(StockEndpoint);
    }

    [Fact]
    public async Task GetBondPositions_Unauthenticated_Returns_Unauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(BondEndpoint);
    }

    [Fact]
    public async Task GetBondPositions_Authenticated_Returns_Ok()
    {
        await Get_Authenticated_Returns_Ok(BondEndpoint);
    }

    [Fact]
    public async Task GetIndexPositions_Unauthenticated_Returns_Unauthorized()
    {
        await Get_Unauthenticated_Returns_Unauthorized(IndexEndpoint);
    }

    [Fact]
    public async Task GetIndexPositions_Authenticated_Returns_Ok()
    {
        await Get_Authenticated_Returns_Ok(IndexEndpoint);
    }
}