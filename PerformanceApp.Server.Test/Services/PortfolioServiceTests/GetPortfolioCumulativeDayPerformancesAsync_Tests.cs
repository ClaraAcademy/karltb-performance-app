using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using PerformanceApp.Data.Constants.PerformanceType;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class GetPortfolioCumulativeDayPerformancesAsync_Tests() : PortfolioServiceTestFixture()
{
    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var portfolioId = 1;
        var portfolioName = "Portfolio 1";

        var performanceType = new PerformanceTypeBuilder()
            .WithId(5)
            .WithName(PerformanceTypeConstants.CumulativeDay)
            .Build();
        var performances = Enumerable.Range(1, 3)
            .Select(i => new PortfolioPerformanceBuilder()
                .WithId(i)
                .WithPeriodStart(new DateOnly(2025, 1, i))
                .WithPeriodEnd(new DateOnly(2025, 1, i))
                .WithPerformanceType(performanceType)
                .WithValue(i * 100m)
                .Build())
            .ToList();
        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithPerformances(performances)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var actual = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);

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
        var portfolioId = 1;
        var portfolioName = "Portfolio 1";

        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithPerformances([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenNoPortfolio()
    {
        // Arrange
        var portfolioId = 1;
        var portfolio = null as Portfolio;

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }


}