using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioRepositoryTest
{

    [Fact]
    public async Task AddPortfoliosAsync_AddsMultiplePortfoliosToDatabase()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>
        {
            new Portfolio { Name = "Async Portfolio 1" },
            new Portfolio { Name = "Async Portfolio 2" }
        };

        await repository.AddPortfoliosAsync(portfolios);

        Assert.Equal(2, context.Portfolios.Count());
    }

    [Fact]
    public async Task AddPortfoliosAsync_DoesNotAddEmptyList()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>();

        await repository.AddPortfoliosAsync(portfolios);

        Assert.Equal(0, context.Portfolios.Count());
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsCorrectPortfolio()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolio = new Portfolio { Name = "Get By ID Portfolio" };
        context.Portfolios.Add(portfolio);
        context.SaveChanges();

        var retrievedPortfolio = await repository.GetPortfolioAsync(portfolio.Id);
        Assert.NotNull(retrievedPortfolio);
        Assert.Equal(portfolio.Name, retrievedPortfolio.Name);
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsNull_WhenNotFound()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var retrievedPortfolio = await repository.GetPortfolioAsync(999);
        Assert.Null(retrievedPortfolio);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByNames_ReturnsCorrectPortfolios()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>
        {
            new Portfolio { Name = "Portfolio A" },
            new Portfolio { Name = "Portfolio B" },
            new Portfolio { Name = "Portfolio C" }
        };
        context.Portfolios.AddRange(portfolios);
        context.SaveChanges();

        var namesToRetrieve = new List<string> { "Portfolio A", "Portfolio C" };
        var retrievedPortfolios = await repository.GetPortfoliosAsync(namesToRetrieve);

        Assert.Equal(2, retrievedPortfolios.Count());
        Assert.Contains(retrievedPortfolios, p => p.Name == "Portfolio A");
        Assert.Contains(retrievedPortfolios, p => p.Name == "Portfolio C");
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByNames_ReturnsEmpty_WhenNoMatches()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>
        {
            new Portfolio { Name = "Portfolio X" },
            new Portfolio { Name = "Portfolio Y" }
        };
        context.Portfolios.AddRange(portfolios);
        context.SaveChanges();

        var namesToRetrieve = new List<string> { "Portfolio A", "Portfolio B" };
        var retrievedPortfolios = await repository.GetPortfoliosAsync(namesToRetrieve);

        Assert.Empty(retrievedPortfolios);
    }

    [Fact]
    public async Task GetProperPortfoliosAsync_ReturnsPortfoliosWithBenchmarks()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var benchmarkPortfolio = new Portfolio { Name = "Benchmark Portfolio" };
        var portfolioWithBenchmark = new Portfolio { Name = "With Benchmark" };
        var portfolioWithoutBenchmark = new Portfolio { Name = "Without Benchmark" };
        context.Portfolios.AddRange(benchmarkPortfolio, portfolioWithBenchmark, portfolioWithoutBenchmark);
        context.SaveChanges();

        var benchmark = new Benchmark { PortfolioId = portfolioWithBenchmark.Id, BenchmarkId = benchmarkPortfolio.Id };
        context.Benchmarks.Add(benchmark);
        context.SaveChanges();

        var properPortfolios = await repository.GetProperPortfoliosAsync();

        Assert.Single(properPortfolios);
        Assert.Equal("With Benchmark", properPortfolios.First().Name);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ReturnsAllPortfolios()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>
        {
            new Portfolio { Name = "Portfolio 1" },
            new Portfolio { Name = "Portfolio 2" }
        };
        context.Portfolios.AddRange(portfolios);
        context.SaveChanges();

        var allPortfolios = await repository.GetPortfoliosAsync();

        Assert.Equal(2, allPortfolios.Count());
    }

    [Fact]
    public async Task GetPortfoliosAsync_ReturnsEmpty_WhenNoPortfoliosExist()
    {
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var allPortfolios = await repository.GetPortfoliosAsync();

        Assert.Empty(allPortfolios);
    }

}