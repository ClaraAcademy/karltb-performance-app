using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using PerformanceApp.Data.Constants;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class GetPortfolioBenchmarkCumulativeDayPerformancesAsync_Tests()
    : PortfolioServiceTestFixture()
{
    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var n = 9;
        var performanceType = new PerformanceTypeBuilder()
            .WithName(PerformanceTypeConstants.CumulativeDay)
            .Build();
        var portfolioPerformances = new PortfolioPerformanceBuilder()
            .WithPerformanceType(performanceType)
            .Many(n)
            .ToList();
        var benchmarkPerformances = new PortfolioPerformanceBuilder()
            .WithPerformanceType(performanceType)
            .WithValue(-100m)
            .Many(n)
            .ToList();
        var benchmark = new PortfolioBuilder()
            .WithName(PortfolioBuilderDefaults.BenchmarkName)
            .WithId(PortfolioBuilderDefaults.BenchmarkId)
            .WithPerformances(benchmarkPerformances)
            .Build();
        var portfolio = new PortfolioBuilder()
            .WithPerformances(portfolioPerformances)
            .WithBenchmarks([benchmark])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var actual = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(1);

        // Assert
        Assert.Equal(portfolioPerformances.Count, actual.Count);
        for (int i = 0; i < portfolioPerformances.Count; i++)
        {
            var expectedPortfolioPerformance = portfolioPerformances[i];
            var expectedBenchmarkPerformance = benchmarkPerformances
                .FirstOrDefault(bp => bp.PeriodStart == expectedPortfolioPerformance.PeriodStart);

            Assert.NotNull(expectedBenchmarkPerformance);

            var actualDto = actual[i];
            Assert.Equal(expectedPortfolioPerformance.PeriodStart, actualDto.Bankday);
            Assert.Equal(expectedPortfolioPerformance.Value, actualDto.PortfolioValue);
            Assert.Equal(expectedBenchmarkPerformance.Value, actualDto.BenchmarkValue);
        }
    }

    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenNoBenchmarks()
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
            .WithBenchmarks([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenInvalidPortfolioId()
    {
        // Arrange
        var portfolio = null as Portfolio;

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(-1);

        // Assert
        Assert.Empty(result);
    }


}