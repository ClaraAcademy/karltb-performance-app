using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioRepositoryTest : BaseRepositoryTest
{
    private readonly PortfolioRepository _repository;

    public PortfolioRepositoryTest()
    {
        _repository = new PortfolioRepository(_context);
    }

    private static string CreateName(int i) => $"Portfolio {i}";
    private static Portfolio CreatePortfolio(int i) => new Portfolio { Id = i, Name = CreateName(i) };
    private static List<Portfolio> CreatePortfolios(int count)
    {
        return Enumerable.Range(1, count)
            .Select(CreatePortfolio)
            .ToList();
    }

    [Fact]
    public async Task AddPortfoliosAsync_AddsMultiplePortfoliosToDatabase()
    {
        // Arrange
        var n = 24;
        var portfolios = CreatePortfolios(n);

        // Act
        await _repository.AddPortfoliosAsync(portfolios);

        // Assert
        var fetched = _context.Portfolios.ToList();
        var actual = fetched.Count;

        Assert.Equal(n, actual);
    }

    [Fact]
    public async Task AddPortfoliosAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var portfolios = new List<Portfolio>();
        var expected = 0;

        // Act
        await _repository.AddPortfoliosAsync(portfolios);
        var actual = _context.Portfolios.Count();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsCorrectPortfolio()
    {
        // Arrange
        var n = 51;
        var portfolios = CreatePortfolios(n);

        // Act
        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        var id = 23;
        var fetched = await _repository.GetPortfolioAsync(id);

        // Assert
        Assert.NotNull(fetched);
        var expected = CreateName(id);
        var actual = fetched.Name;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsNull_WhenNotFound()
    {
        // Act
        var retrievedPortfolio = await _repository.GetPortfolioAsync(999);

        // Assert
        Assert.Null(retrievedPortfolio);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByNames_ReturnsCorrectPortfolios()
    {
        // Arrange
        var nTotal = 4;
        var portfolios = CreatePortfolios(nTotal);
        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        // Act
        var nToRetrieve = 2;
        var namesToFetch = Enumerable.Range(1, nToRetrieve).Select(CreateName).ToList();
        var fetched = await _repository.GetPortfoliosAsync(namesToFetch);

        // Assert
        var nExpected = nToRetrieve;
        var nActual = fetched.Count();
        Assert.Equal(nExpected, nActual);
        foreach (var name in namesToFetch)
        {
            Assert.Contains(fetched, p => p.Name == name);
        }
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByNames_ReturnsEmpty_WhenNoMatches()
    {
        // Arrange
        var nPortfolios = 10;
        var portfolios = CreatePortfolios(nPortfolios);

        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        // Act
        var namesToFetch = new List<string> { CreateName(-100), CreateName(-200) };
        var fetched = await _repository.GetPortfoliosAsync(namesToFetch);

        // Assert
        Assert.Empty(fetched);
    }

    [Fact]
    public async Task GetProperPortfoliosAsync_ReturnsPortfoliosWithBenchmarks()
    {
        // Arrange
        var benchmarkPortfolio = new Portfolio { Name = "Benchmark Portfolio" };
        var portfolioWithBenchmark = new Portfolio { Name = "With Benchmark" };
        var portfolioWithoutBenchmark = new Portfolio { Name = "Without Benchmark" };
        var portfolios = new List<Portfolio> { benchmarkPortfolio, portfolioWithBenchmark, portfolioWithoutBenchmark };

        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        var benchmark = new Benchmark { PortfolioId = portfolioWithBenchmark.Id, BenchmarkId = benchmarkPortfolio.Id };
        await _context.Benchmarks.AddAsync(benchmark);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetProperPortfoliosAsync();

        Assert.Single(fetched);
        Assert.Equal("With Benchmark", fetched.First().Name);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ReturnsAllPortfolios()
    {
        // Arrange
        var nExpected = 20;
        var expected = CreatePortfolios(nExpected);

        await _context.Portfolios.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetPortfoliosAsync();

        // Assert
        var nActual = actual.Count();
        Assert.Equal(nExpected, nActual);
        foreach (var portfolio in expected)
        {
            Assert.Contains(actual, p => p.Name == portfolio.Name);
        }
    }

    [Fact]
    public async Task GetPortfoliosAsync_ReturnsEmpty_WhenNoPortfoliosExist()
    {
        // Act
        var actual = await _repository.GetPortfoliosAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByUserId_ReturnsCorrectPortfolios()
    {
        // Arrange
        var userId1 = "user1";
        var userId2 = "user2";

        var portfolio1 = new Portfolio { Name = "Portfolio 1", UserID = userId1 };
        var portfolio2 = new Portfolio { Name = "Portfolio 2", UserID = userId1 };
        var portfolio3 = new Portfolio { Name = "Portfolio 3", UserID = userId2 };

        var portfolios = new List<Portfolio> { portfolio1, portfolio2, portfolio3 };

        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        var benchmark = new Benchmark { PortfolioId = portfolio1.Id, BenchmarkId = portfolio3.Id };
        await _context.Benchmarks.AddAsync(benchmark);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetPortfoliosAsync(userId1);

        // Assert
        var nExpected = 1; // Only portfolio1 has a benchmark
        var nActual = fetched.Count();
        Assert.Equal(nExpected, nActual);
        Assert.Contains(fetched, p => p.Name == "Portfolio 1");
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByUserId_ReturnsEmpty_WhenNoMatchingPortfolios()
    {
        // Arrange
        var userId = "nonexistent_user";

        // Act
        var fetched = await _repository.GetPortfoliosAsync(userId);

        // Assert
        Assert.Empty(fetched);
    }

}