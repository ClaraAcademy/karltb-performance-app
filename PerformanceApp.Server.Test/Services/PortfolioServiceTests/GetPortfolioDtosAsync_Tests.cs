using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class PortfolioDtosAsync_Tests() : PortfolioServiceTestFixture
{
    [Fact]
    public async Task GetPortfolioDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var expected = new PortfolioBuilder().Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync([expected]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync();
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(expected.Name, actual.PortfolioName);
        Assert.Equal(expected.Id, actual.PortfolioId);
    }

    [Fact]
    public async Task GetPortfolioDTOsAsync_ReturnsEmptyList_WhenNoPortfolios()
    {
        // Arrange
        _portfolioRepositoryMock
            .Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioDTOSAsync_ByUserId_ReturnsExpectedResults()
    {
        // Arrange
        var userId = "user-123";
        var expected = new PortfolioBuilder()
            .WithUser(new ApplicationUser { Id = userId })
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfoliosAsync(userId))
            .ReturnsAsync([expected]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync(userId);
        var actual = result.Single();

        // Assert
        Assert.Single(result);
        Assert.Equal(expected.Name, actual.PortfolioName);
        Assert.Equal(expected.Id, actual.PortfolioId);
    }

    [Fact]
    public async Task GetPortfolioDTOSAsync_ByUserId_ReturnsEmptyList_WhenNoMatchingPortfolios()
    {
        // Arrange
        var userId = "nonexistent-user";

        _portfolioRepositoryMock
            .Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync(userId);

        // Assert
        Assert.Empty(result);
    }

}