using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Test.Controllers.Integration;

public class DateInfoControllerIntegrationTest(WebApplicationFactory<Program> factory) : BaseControllerIntegrationTests(factory)
{
    private readonly string _baseUrl = "api/DateInfo";

    [Fact]
    public async Task GetDates_Unauthenticated_Returns_Unauthorized()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetDates_Authenticated_Returns_Ok_With_Data()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);
        AddAuthorizationHeader(request, TestUser);

        // Act
        var response = await _client.SendAsync(request);

        // Assert
        response.EnsureSuccessStatusCode();
        var bankdayDtos = await response.Content.ReadFromJsonAsync<List<BankdayDTO>>();
        Assert.NotNull(bankdayDtos);
        Assert.NotEmpty(bankdayDtos);
    }
}