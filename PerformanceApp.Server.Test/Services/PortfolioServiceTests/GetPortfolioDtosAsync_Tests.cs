using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class PortfolioDtosAsync_Tests() : PortfolioServiceTestFixture
{
    [Fact]
    public async Task GetPortfolioDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var expected = PortfolioBuilderDefaults.Portfolio;

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
        var expected = new PortfolioBuilder()
            .WithUser(ApplicationUserBuilderDefaults.User)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfoliosAsync(It.IsAny<string>()))
            .ReturnsAsync([expected]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync("some-user-id");
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
        _portfolioRepositoryMock
            .Setup(r => r.GetPortfoliosAsync(It.IsAny<string>()))
            .ReturnsAsync([]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync("some-user-id");

        // Assert
        Assert.Empty(result);
    }

}