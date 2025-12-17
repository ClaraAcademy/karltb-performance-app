using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class PortfolioRepositoryTest : BaseRepositoryTest
{
    private readonly PortfolioRepository _repository;

    public PortfolioRepositoryTest()
    {
        _repository = new PortfolioRepository(_context);
    }

    [Fact]
    public async Task AddPortfoliosAsync_AddsMultiplePortfoliosToDatabase()
    {
        // Arrange
        var expected = new PortfolioBuilder()
            .Many(10)
            .ToList();

        // Act
        await _repository.AddPortfoliosAsync(expected);

        // Assert
        var actual = _context.Portfolios.ToList();

        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task AddPortfoliosAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var empty = new List<Portfolio>();

        // Act
        await _repository.AddPortfoliosAsync(empty);
        var actual = _context.Portfolios.ToList();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsCorrectPortfolio()
    {
        // Arrange
        var portfolios = new PortfolioBuilder()
            .Many(5)
            .ToList();
        var expected = portfolios.First();

        // Act
        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        var actual = await _repository.GetPortfolioAsync(expected.Id);

        // Assert
        Assert.NotNull(actual);
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
        var n = 9;
        var m = 4;

        var portfolios = new PortfolioBuilder()
            .Many(n)
            .ToList();

        await _context
            .Portfolios
            .AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        // Act
        var expected = portfolios.Take(m).ToList();
        var names = expected
            .Select(p => p.Name)
            .ToList();
        var fetched = await _repository.GetPortfoliosAsync(names);
        var actual = fetched.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByNames_ReturnsEmpty_WhenNoMatches()
    {
        // Arrange
        var portfolios = new PortfolioBuilder()
            .Many(5)
            .ToList();

        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetPortfoliosAsync(["Nonsense"]);

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetProperPortfoliosAsync_ReturnsPortfoliosWithBenchmarks()
    {
        // Arrange
        var benchmark = new PortfolioBuilder()
            .WithName("Benchmark Portfolio")
            .Build();
        var portfolioWithBenchmark = new PortfolioBuilder()
            .WithName("With Benchmark")
            .WithBenchmark(benchmark)
            .Build();
        var portfolioWithoutBenchmark = new PortfolioBuilder()
            .WithName("Without Benchmark")
            .Build();
        var portfolios = new List<Portfolio> { benchmark, portfolioWithBenchmark, portfolioWithoutBenchmark };

        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetProperPortfoliosAsync();
        var actual = fetched.ToList();

        Assert.Single(actual);
        Assert.Equal(portfolioWithBenchmark.Name, actual[0].Name);
        Assert.Equal(portfolioWithBenchmark.Id, actual[0].Id);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ReturnsAllPortfolios()
    {
        // Arrange
        var expected = new PortfolioBuilder()
            .Many(15)
            .ToList();

        await _context.Portfolios.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetPortfoliosAsync();
        var actual = fetched.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
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
        var user = new ApplicationUserBuilder()
            .WithId("SomeUserId")
            .Build();
        var benchmark = new PortfolioBuilder()
            .WithName("Benchmark Portfolio")
            .Build();
        var portfolio1 = new PortfolioBuilder()
            .WithName("Portfolio 1")
            .WithUser(user)
            .WithBenchmark(benchmark)
            .Build();
        var portfolio2 = new PortfolioBuilder()
            .WithName("Portfolio 2")
            .Build();
        var portfolios = new List<Portfolio> { portfolio1, portfolio2 };
        var expected = new List<Portfolio> { portfolio1 };

        await _context.Portfolios.AddRangeAsync(portfolios);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetPortfoliosAsync(user.Id);

        // Assert
        Assert.Single(actual);
        Assert.Equal(actual, expected);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByUserId_ReturnsEmpty_WhenNoMatchingPortfolios()
    {
        // Arrange
        var userId = "nonsense";

        // Act
        var actual = await _repository.GetPortfoliosAsync(userId);

        // Assert
        Assert.Empty(actual);
    }

}