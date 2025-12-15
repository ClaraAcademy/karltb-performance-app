using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using PerformanceApp.Data.Constants;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class GetPortfolioCumulativeDayPerformancesAsync_Tests() : PortfolioServiceTestFixture()
{
    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var performanceType = new PerformanceTypeBuilder()
            .WithName(PerformanceTypeConstants.CumulativeDay)
            .Build();
        var performances = new PortfolioPerformanceBuilder()
            .WithPerformanceType(performanceType)
            .Many(7)
            .ToList();
        var portfolio = new PortfolioBuilder()
            .WithPerformances(performances)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var actual = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(1);

        // Assert
        Assert.Equal(performances.Count, actual.Count);
        for (int i = 0; i < performances.Count; i++)
        {
            Assert.Equal(performances[i].PeriodStart, actual[i].Bankday);
            Assert.Equal(performances[i].Value, actual[i].Value);
        }
    }

    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenNoPerformances()
    {
        // Arrange
        var portfolio = new PortfolioBuilder()
            .WithPerformances([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenNoPortfolio()
    {
        // Arrange
        var portfolio = null as Portfolio;

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(1);
        // Assert
        Assert.Empty(result);
    }


}