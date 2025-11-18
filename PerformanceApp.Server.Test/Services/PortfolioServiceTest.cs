using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;
using Moq;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Server.Test.Services;

public class PortfolioServiceTest
{
    private readonly Mock<IPortfolioRepository> _portfolioRepositoryMock;
    private readonly Mock<IBenchmarkRepository> _benchmarkRepositoryMock;
    private readonly PortfolioService _portfolioService;

    public PortfolioServiceTest()
    {
        _portfolioRepositoryMock = new Mock<IPortfolioRepository>();
        _benchmarkRepositoryMock = new Mock<IBenchmarkRepository>();
        _portfolioService = new PortfolioService(_portfolioRepositoryMock.Object, _benchmarkRepositoryMock.Object);
    }


    // Factory methods for reusable model creation
    private static Portfolio CreatePortfolio(int id, string name, List<PortfolioPerformance>? performances = null)
    {
        return new Portfolio
        {
            Id = id,
            Name = name,
            PortfolioPerformancesNavigation = performances ?? new List<PortfolioPerformance>()
        };
    }

    private static Benchmark CreateBenchmark(int portfolioId, string portfolioName, int benchmarkId, string benchmarkName)
    {
        return new Benchmark
        {
            PortfolioId = portfolioId,
            PortfolioPortfolioNavigation = CreatePortfolio(portfolioId, portfolioName),
            BenchmarkPortfolioNavigation = CreatePortfolio(benchmarkId, benchmarkName)
        };
    }

    private static PerformanceType CreatePerformanceType(string name)
    {
        return new PerformanceType { Name = name };
    }

    private static PortfolioPerformance CreatePortfolioPerformance(DateOnly date, decimal value, string typeName)
    {
        return new PortfolioPerformance
        {
            PeriodStart = date,
            Value = value,
            PerformanceTypeNavigation = CreatePerformanceType(typeName)
        };
    }

    private static List<Portfolio> GetPortfolios(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => CreatePortfolio(i, $"Portfolio {i}"))
            .ToList();
    }

    private static List<Benchmark> GetBenchmarks(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => CreateBenchmark(i, $"Portfolio {i}", i + 100, $"Benchmark {i}"))
            .ToList();
    }

    [Fact]
    public async Task GetPortfolioDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        var portfolios = GetPortfolios(2);

        _portfolioRepositoryMock.Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync(portfolios);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Portfolio 1", result[0].PortfolioName);
        Assert.Equal("Portfolio 2", result[1].PortfolioName);
    }

    [Fact]
    public async Task GetPortfolioDTOsAsync_ReturnsEmptyList_WhenNoPortfolios()
    {
        // Arrange
        _portfolioRepositoryMock.Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_ReturnsExpectedResults()
    {
        // Arrange
        var benchmarkMappings = GetBenchmarks(2);

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(benchmarkMappings);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Benchmark 1", result[0].BenchmarkName);
        Assert.Equal("Benchmark 2", result[1].BenchmarkName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_ReturnsEmptyList_WhenNoBenchmarks()
    {
        // Arrange
        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(new List<Benchmark>());

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithPortfolioId_ReturnsExpectedResults()
    {
        // Arrange
        int portfolioId = 1;
        var benchmarkMappings = GetBenchmarks(2);

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(benchmarkMappings);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(portfolioId);

        // Assert
        Assert.Single(result);
        Assert.Equal("Benchmark 1", result[0].BenchmarkName);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithPortfolioId_ReturnsEmptyList_WhenNoBenchmarks()
    {
        // Arrange
        int portfolioId = 1;
        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(new List<Benchmark>());

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GeyPortfolioBenchmarksAsync_WithPortfolioId_ReturnsEmptyList_WhenNoMatchingBenchmarks()
    {
        // Arrange
        int portfolioId = 1;
        var benchmarkMappings = new List<Benchmark>
        {
            new Benchmark
            {
                PortfolioId = 2,
                PortfolioPortfolioNavigation = new Portfolio { Id = 2, Name = "Portfolio 2" },
                BenchmarkPortfolioNavigation = new Portfolio { Id = 3, Name = "Benchmark 2" }
            }
        };

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(benchmarkMappings);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarksAsync_WithPortfolioId_ReturnsEmptyList_WhenInvalidPortfolioId()
    {
        // Arrange
        int portfolioId = 999; // Non-existent portfolio ID
        var benchmarkMappings = new List<Benchmark>
        {
            new Benchmark
            {
                PortfolioId = 1,
                PortfolioPortfolioNavigation = new Portfolio { Id = 1, Name = "Portfolio 1" },
                BenchmarkPortfolioNavigation = new Portfolio { Id = 2, Name = "Benchmark 1" }
            }
        };

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(benchmarkMappings);

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        int portfolioId = 1;
        var performanceType = new PerformanceType { Name = PerformanceTypeData.CumulativeDayPerformance };
        var performances = new List<PortfolioPerformance>
        {
            new PortfolioPerformance
            {
                PeriodStart = new DateOnly(2025, 1, 1),
                Value = 100m,
                PerformanceTypeNavigation = performanceType
            },
            new PortfolioPerformance
            {
                PeriodStart = new DateOnly(2025, 1, 2),
                Value = 200m,
                PerformanceTypeNavigation = performanceType
            }
        };
        var portfolio = new Portfolio
        {
            Id = portfolioId,
            PortfolioPerformancesNavigation = performances
        };

        _portfolioRepositoryMock.Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync(portfolio);

        // Act
        var result = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new DateOnly(2025, 1, 1), result[0].Bankday);
        Assert.Equal(100m, result[0].Value);
        Assert.Equal(new DateOnly(2025, 1, 2), result[1].Bankday);
        Assert.Equal(200m, result[1].Value);

    }
}