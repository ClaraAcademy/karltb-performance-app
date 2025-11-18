using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;
using Moq;

namespace PerformanceApp.Server.Test.Services;

public class PerformanceServiceTest
{
    private readonly Mock<IKeyFigureValueRepository> _keyFigureValueRepositoryMock;
    private readonly Mock<IPortfolioService> _portfolioServiceMock;
    private readonly PerformanceService _performanceService;

    public PerformanceServiceTest()
    {
        _keyFigureValueRepositoryMock = new Mock<IKeyFigureValueRepository>();
        _portfolioServiceMock = new Mock<IPortfolioService>();
        _performanceService = new PerformanceService(_keyFigureValueRepositoryMock.Object, _portfolioServiceMock.Object);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_ReturnsExpectedResults()
    {
        // Arrange
        int portfolioId = 1;
        var portfolioBenchmarkDtos = new List<PortfolioBenchmarkDTO>
        {
            new PortfolioBenchmarkDTO { PortfolioId = 1, PortfolioName = "Portfolio 1", BenchmarkId = 2, BenchmarkName = "Benchmark 1" }
        };

        var keyFigureInfos = new List<KeyFigureInfo>
        {
            new KeyFigureInfo { Id = 1, Name = "Key Figure 1" }
        };

        var portfolioKeyFigureValues = new List<KeyFigureValue>
        {
            new KeyFigureValue { PortfolioId = 1, KeyFigureId = 1, Value = 10.0m }
        };

        var benchmarkKeyFigureValues = new List<KeyFigureValue>
        {
            new KeyFigureValue { PortfolioId = 2, KeyFigureId = 1, Value = 20.0m }
        };

        _portfolioServiceMock.Setup(s => s.GetPortfolioBenchmarksAsync(portfolioId))
            .ReturnsAsync(portfolioBenchmarkDtos);

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureInfosAsync())
            .ReturnsAsync(keyFigureInfos);

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureValuesAsync(portfolioId))
            .ReturnsAsync(portfolioKeyFigureValues);

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureValuesAsync(2))
            .ReturnsAsync(benchmarkKeyFigureValues);

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

        // Assert
        Assert.Single(result);
        var dto = result.First();
        Assert.Equal(1, dto.PortfolioId);
        Assert.Equal("Portfolio 1", dto.PortfolioName);
        Assert.Equal(2, dto.BenchmarkId);
        Assert.Equal("Benchmark 1", dto.BenchmarkName);
        Assert.Equal(1, dto.KeyFigureId);
        Assert.Equal("Key Figure 1", dto.KeyFigureName);
        Assert.Equal(10.0m, dto.PortfolioValue);
        Assert.Equal(20.0m, dto.BenchmarkValue);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_NoKeyFigureValues_ReturnsEmpty()
    {
        // Arrange
        int portfolioId = 1;
        var portfolioBenchmarkDtos = new List<PortfolioBenchmarkDTO>
        {
            new PortfolioBenchmarkDTO { PortfolioId = 1, PortfolioName = "Portfolio 1", BenchmarkId = 2, BenchmarkName = "Benchmark 1" }
        };

        var keyFigureInfos = new List<KeyFigureInfo>
        {
            new KeyFigureInfo { Id = 1, Name = "Key Figure 1" }
        };

        _portfolioServiceMock.Setup(s => s.GetPortfolioBenchmarksAsync(portfolioId))
            .ReturnsAsync(portfolioBenchmarkDtos);

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureInfosAsync())
            .ReturnsAsync(keyFigureInfos);

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureValuesAsync(portfolioId))
            .ReturnsAsync(new List<KeyFigureValue>());

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureValuesAsync(2))
            .ReturnsAsync(new List<KeyFigureValue>());

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

        _portfolioServiceMock.Setup(s => s.GetPortfolioBenchmarksAsync(portfolioId))
            .ReturnsAsync(new List<PortfolioBenchmarkDTO>());

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureInfosAsync())
            .ReturnsAsync(new List<KeyFigureInfo>());

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkKeyFigureValues_InvalidPortfolioId_ReturnsEmptyList()
    {
        // Arrange
        int portfolioId = -1;

        _portfolioServiceMock.Setup(s => s.GetPortfolioBenchmarksAsync(portfolioId))
            .ReturnsAsync(new List<PortfolioBenchmarkDTO>());

        _keyFigureValueRepositoryMock.Setup(r => r.GetKeyFigureInfosAsync())
            .ReturnsAsync(new List<KeyFigureInfo>());

        // Act
        var result = await _performanceService.GetPortfolioBenchmarkKeyFigureValues(portfolioId);

        // Assert
        Assert.Empty(result);
    }
}