using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
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
            BenchmarkId = benchmarkId,
            BenchmarkPortfolioNavigation = CreatePortfolio(benchmarkId, benchmarkName)
        };
    }

    private static Benchmark CreateBenchmark(Portfolio portfolio, Portfolio benchmark)
    {
        return new Benchmark
        {
            PortfolioId = portfolio.Id,
            PortfolioPortfolioNavigation = portfolio,
            BenchmarkId = benchmark.Id,
            BenchmarkPortfolioNavigation = benchmark
        };
    }

    private static PerformanceType CreatePerformanceType(string name)
    {
        return new PerformanceType { Name = name };
    }

    private static PortfolioPerformance CreatePortfolioPerformance(DateOnly date, decimal value, PerformanceType type)
    {
        return new PortfolioPerformance
        {
            PeriodStart = date,
            Value = value,
            PerformanceTypeNavigation = type
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

    private static List<PortfolioPerformance> GetPortfolioPerformances(int count, PerformanceType type)
    {
        return Enumerable.Range(1, count)
            .Select(i => CreatePortfolioPerformance(new DateOnly(2025, 1, i), i * 100m, type))
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

        _portfolioRepositoryMock
            .Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync(new List<Portfolio>
            {
                new Portfolio
                {
                    Id = 1,
                    Name = "Portfolio 1",
                    PortfolioPortfolioBenchmarkEntityNavigation = new List<Benchmark>
                    {
                        benchmarkMappings[0]
                    }
                },
                new Portfolio
                {
                    Id = 2,
                    Name = "Portfolio 2",
                    PortfolioPortfolioBenchmarkEntityNavigation = new List<Benchmark>
                    {
                        benchmarkMappings[1]
                    }
                }
            });

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
    public async Task GetPortfolioBenchmarksAsync_WithUserId_ReturnsExpectedResults()
    {
        // Arrange
        var userId = "user-123";
        var portfolio1 = new Portfolio
        {
            Id = 1,
            Name = "Portfolio 1",
            UserID = userId,
            PortfolioPortfolioBenchmarkEntityNavigation = new List<Benchmark>
        {
            CreateBenchmark(1, "Portfolio 1", 101, "Benchmark 1"),
            CreateBenchmark(1, "Portfolio 1", 102, "Benchmark 2")
        }
        };

        var portfolio2 = new Portfolio
        {
            Id = 2,
            Name = "Portfolio 2",
            UserID = "other-user",
            PortfolioPortfolioBenchmarkEntityNavigation = new List<Benchmark>
        {
            CreateBenchmark(2, "Portfolio 2", 103, "Benchmark 3")
        }
        };

        _portfolioRepositoryMock.Setup(r => r.GetPortfoliosAsync(userId))
            .ReturnsAsync(new List<Portfolio> { portfolio1 });

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarksAsync(userId);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, dto => dto.BenchmarkName == "Benchmark 1");
        Assert.Contains(result, dto => dto.BenchmarkName == "Benchmark 2");

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
        int portfolioId = -999;
        var benchmarkMappings = GetBenchmarks(5);

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
        var performanceType = CreatePerformanceType(PerformanceTypeData.CumulativeDayPerformance);
        var performances = GetPortfolioPerformances(2, performanceType);

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

    [Fact]
    public async Task GetPortfolioCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenNoPerformances()
    {
        // Arrange
        int portfolioId = 1;
        var portfolio = new Portfolio
        {
            Id = portfolioId,
            PortfolioPerformancesNavigation = new List<PortfolioPerformance>()
        };

        _portfolioRepositoryMock.Setup(r => r.GetPortfolioAsync(portfolioId))
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
        int portfolioId = 1;

        _portfolioRepositoryMock.Setup(r => r.GetPortfolioAsync(portfolioId))
            .ReturnsAsync((Portfolio?)null);

        // Act
        var result = await _portfolioService.GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBenchmarkAsync_ReturnsExpectedResult()
    {
        // Arrange
        var portfolioId = 1;
        var benchmarkId = 200;
        var portfolioName = "Portfolio 1";
        var benchmarkName = "Benchmark 1";

        var benchmarkMapping = CreateBenchmark(portfolioId, portfolioName, benchmarkId, benchmarkName);

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync([benchmarkMapping]);

        // Act
        var result = await _portfolioService.GetBenchmarkAsync(portfolioId);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(benchmarkName, result.BenchmarkPortfolioNavigation.Name);
        Assert.Equal(benchmarkId, result.BenchmarkId);
    }

    [Fact]
    public async Task GetBenchmarkAsync_ReturnsNull_WhenNoBenchmarkMapping()
    {
        // Arrange
        int portfolioId = 1;

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(new List<Benchmark>());

        // Act
        var result = await _portfolioService.GetBenchmarkAsync(portfolioId);

        // Assert
        Assert.Null(result);
    }

    private void SetupGetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync()
    {
        var bankday = new DateOnly(2025, 1, 1);
        var performanceType = CreatePerformanceType(PerformanceTypeData.CumulativeDayPerformance);
        var portfolioPerformance = new PortfolioPerformance { PeriodStart = bankday, Value = 100m, PerformanceTypeNavigation = performanceType };
        var benchmarkPerformance = new PortfolioPerformance { PeriodStart = bankday, Value = 150m, PerformanceTypeNavigation = performanceType };

        var portfolio = new Portfolio { Id = 1, Name = "Portfolio", PortfolioPerformancesNavigation = [portfolioPerformance] };
        var benchmark = new Portfolio { Id = 2, Name = "Benchmark", PortfolioPerformancesNavigation = [benchmarkPerformance] };

        var benchmarkMapping = CreateBenchmark(portfolio, benchmark);

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(new List<Benchmark> { benchmarkMapping });

        _portfolioRepositoryMock.Setup(r => r.GetPortfolioAsync(portfolio.Id))
            .ReturnsAsync(portfolio);

        _portfolioRepositoryMock.Setup(r => r.GetPortfolioAsync(benchmark.Id))
            .ReturnsAsync(benchmark);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsExpectedResults()
    {
        // Arrange
        SetupGetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync();
        var portfolioId = 1;

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Single(result);
        var dto = result.First();
        Assert.Equal(new DateOnly(2025, 1, 1), dto.Bankday);
        Assert.Equal(100m, dto.PortfolioValue);
        Assert.Equal(150m, dto.BenchmarkValue);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenNoBenchmarks()
    {
        // Arrange
        int portfolioId = 1;

        _benchmarkRepositoryMock.Setup(r => r.GetBenchmarkMappingsAsync())
            .ReturnsAsync(new List<Benchmark>());

        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync_ReturnsEmptyList_WhenInvalidPortfolioId()
    {
        // Arrange
        int portfolioId = -999;

        SetupGetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync();
        // Act
        var result = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPortfolioDTOSAsync_ByUserId_ReturnsExpectedResults()
    {
        // Arrange
        var userId = "user-123";
        var nExpected = 2;
        var portfolio1 = new Portfolio { Id = 1, Name = "Portfolio 1", UserID = userId };
        var portfolio2 = new Portfolio { Id = 2, Name = "Portfolio 2", UserID = userId };
        var portfolio3 = new Portfolio { Id = 3, Name = "Portfolio 3", UserID = "other-user" };

        var portfolios = new List<Portfolio> { portfolio1, portfolio2, portfolio3 };

        _portfolioRepositoryMock.Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync(portfolios);
        _portfolioRepositoryMock.Setup(r => r.GetPortfoliosAsync(userId))
            .ReturnsAsync(portfolios.Where(p => p.UserID == userId).ToList());

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync(userId);

        // Assert
        var nActual = result.Count;
        Assert.Equal(nExpected, nActual);
        for (int i = 1; i <= nExpected; i++)
        {
            Assert.Contains(result, p => p.PortfolioName == $"Portfolio {i}");
            Assert.Contains(result, p => p.PortfolioId == i);
        }
    }

    [Fact]
    public async Task GetPortfolioDTOSAsync_ByUserId_ReturnsEmptyList_WhenNoMatchingPortfolios()
    {
        // Arrange
        var userId = "nonexistent-user";

        _portfolioRepositoryMock.Setup(r => r.GetProperPortfoliosAsync())
            .ReturnsAsync(new List<Portfolio>());

        // Act
        var result = await _portfolioService.GetPortfolioDTOsAsync(userId);

        // Assert
        Assert.Empty(result);
    }
}