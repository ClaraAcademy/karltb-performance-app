using Moq;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Data.Builders;
using PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;
using PerformanceApp.Data.Constants.PerformanceType;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests;

public class GetPortfolioBenchmarkCumulativeDayPerformancesAsync_Tests()
    : PortfolioServiceTestFixture()
{
    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var performanceTypeId = 7;
        var portfolioId = 1;
        var portfolioName = "Portfolio 1";
        var benchmarkId = 2;
        var benchmarkName = "Benchmark 1";
        var performanceType = new PerformanceTypeBuilder()
            .WithId(performanceTypeId)
            .WithName(PerformanceTypeConstants.CumulativeDay)
            .Build();
        var portfolioPerformances = new List<PortfolioPerformance>
        {
            new PortfolioPerformanceBuilder()
                .WithId(performanceTypeId)
                .WithPeriodStart(new DateOnly(2025, 1, 1))
                .WithPeriodEnd(new DateOnly(2025, 1, 1))
                .WithPerformanceType(performanceType)
                .WithValue(100m)
                .Build()
        };
        var benchmarkPerformances = new List<PortfolioPerformance>
        {
            new PortfolioPerformanceBuilder()
                .WithId(performanceTypeId)
                .WithPeriodStart(new DateOnly(2025, 1, 1))
                .WithPeriodEnd(new DateOnly(2025, 1, 1))
                .WithPerformanceType(performanceType)
                .WithValue(150m)
                .Build()
        };
        
        var benchmark = new PortfolioBuilder()
            .WithId(benchmarkId)
            .WithName(benchmarkName)
            .WithPerformances(benchmarkPerformances)
            .Build();

        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithPerformances(portfolioPerformances)
            .WithBenchmarks([benchmark])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var actual = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Equal(portfolioPerformances.Count, actual.Count);
        Assert.Single(actual);
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
        var portfolioId = 1;
        var portfolioName = "Portfolio 1";
        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName(portfolioName)
            .WithBenchmarks([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenInvalidPortfolioId()
    {
        // Arrange
        var portfolioId = 1;

        var portfolio = null as Portfolio;
        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }


}