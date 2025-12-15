using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using PerformanceApp.Data.Builders;
using Moq;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Server.Test.Services;

public class PerformanceServiceTest
{
    private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock = new();
    private readonly IPerformanceService _performanceService;

    public PerformanceServiceTest()
    {
        _portfolioRepositoryMock = new Mock<IPortfolioRepository>();
        _performanceService = new PerformanceService(_portfolioRepositoryMock.Object);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_ReturnsExpectedResults()
    {
        // Arrange
        var keyFigureId = 1;
        var keyFigureName = "Test Key Figure";
        var keyFigureInfo = new KeyFigureInfoBuilder()
            .WithId(keyFigureId)
            .WithName(keyFigureName)
            .Build();
        var portfolioKeyFigure = new KeyFigureValueBuilder()
            .WithKeyFigureInfo(keyFigureInfo)
            .Build();
        var benchmarkKeyFigure = new KeyFigureValueBuilder()
            .WithKeyFigureInfo(keyFigureInfo)
            .WithValue(200.0m)
            .Build();
        var benchmark = new PortfolioBuilder()
            .WithKeyFigureValues([benchmarkKeyFigure])
            .Build();
        var portfolio = new PortfolioBuilder()
            .WithKeyFigureValues([portfolioKeyFigure])
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(1);
        var dto = result.FirstOrDefault();

        // Assert
        Assert.Single(result);
        Assert.NotNull(dto);

        Assert.Equal(keyFigureId, dto.KeyFigureId);
        Assert.Equal(keyFigureName, dto.KeyFigureName);

        Assert.Equal(portfolio.Id, dto.PortfolioId);
        Assert.Equal(benchmark.Id, dto.BenchmarkId);

        Assert.Equal(portfolioKeyFigure.Value, dto.PortfolioValue);
        Assert.Equal(benchmarkKeyFigure.Value, dto.BenchmarkValue);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_NoKeyFigureValues_ReturnsEmpty()
    {
        // Arrange
        var benchmark = PortfolioBuilderDefaults.Benchmark;
        var portfolio = new PortfolioBuilder()
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_NoBenchmark_ReturnsEmptyList()
    {
        // Arrange
        var portfolio = new PortfolioBuilder()
            .WithBenchmarks([])
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(1);
        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_InvalidPortfolioId_ReturnsEmptyList()
    {
        // Arrange
        var portfolio = null as Portfolio;

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(It.IsAny<int>()))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(1);

        // Assert
        Assert.Empty(result);
    }
}