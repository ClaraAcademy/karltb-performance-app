using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using PerformanceApp.Data.Builders;
using Moq;
using PerformanceApp.Infrastructure.Repositories;

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
        var portfolioId = 1;
        var benchmarkId = 100;
        var keyFigureInfo = new KeyFigureInfoBuilder()
            .WithId(1)
            .WithName("Key Figure 1")
            .Build();

        var keyFigureValue = new KeyFigureValueBuilder()
            .WithValue(150.0m)
            .WithKeyFigureInfo(keyFigureInfo)
            .Build();
        
        var benchmark = new PortfolioBuilder()
            .WithId(benchmarkId)
            .WithName("Benchmark")
            .WithKeyFigureValues([keyFigureValue])
            .Build();

        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName("Portfolio 1")
            .WithKeyFigureValues([keyFigureValue])
            .WithBenchmark(benchmark)
            .Build();
            
        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

        // Assert
        Assert.Single(result);
        var dto = result.First();
        Assert.Equal(portfolio.Id, dto.PortfolioId);
        Assert.Equal(portfolio.Name, dto.PortfolioName);
        Assert.Equal(benchmark.Id, dto.BenchmarkId);
        Assert.Equal(benchmark.Name, dto.BenchmarkName);
        Assert.Equal(keyFigureInfo.Id, dto.KeyFigureId);
        Assert.Equal(keyFigureInfo.Name, dto.KeyFigureName);
        Assert.Equal(keyFigureValue.Value, dto.PortfolioValue);
        Assert.Equal(keyFigureValue.Value, dto.BenchmarkValue);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_NoKeyFigureValues_ReturnsEmpty()
    {
        // Arrange
        int portfolioId = 1;
        var benchmark = new PortfolioBuilder()
            .WithId(100)
            .WithName("Benchmark")
            .Build();

        var portfolio = new PortfolioBuilder()
            .WithId(1)
            .WithName("Portfolio 1")
            .WithBenchmark(benchmark)
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_NoCombinations_ReturnsEmptyList()
    {
        // Arrange
        int portfolioId = 1;
        var portfolio = new PortfolioBuilder()
            .WithId(portfolioId)
            .WithName("Portfolio 1")
            .Build();

        _portfolioRepositoryMock
            .Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

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