using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Xunit;

namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioRepositoryTest : RepositoryTest
{
    [Fact]
    public void AddPortfolio_AddsPortfolioToDatabase()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolio = new Portfolio { Name = "Test Portfolio" };

        repository.AddPortfolio(portfolio);

        var retrievedPortfolio = context.Portfolios.Find(portfolio.Id);
        Assert.NotNull(retrievedPortfolio);
    }

    [Fact]
    public async Task AddPortfolioAsync_AddsPortfolioToDatabase()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolio = new Portfolio { Name = "Async Test Portfolio" };

        await repository.AddPortfolioAsync(portfolio);

        var retrievedPortfolio = await context.Portfolios.FindAsync(portfolio.Id);
        Assert.NotNull(retrievedPortfolio);
    }

    [Fact]
    public void GetPortfolio_ReturnsCorrectPortfolio()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolio = new Portfolio { Name = "Get Test Portfolio" };
        context.Portfolios.Add(portfolio);
        context.SaveChanges();

        var retrievedPortfolio = repository.GetPortfolio("Get Test Portfolio");
        Assert.NotNull(retrievedPortfolio);
        Assert.Equal(portfolio.Name, retrievedPortfolio!.Name);
    }

    [Fact]
    public void GetPortfolio_ReturnsNullForNonExistentPortfolio()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var retrievedPortfolio = repository.GetPortfolio("Non Existent Portfolio");
        Assert.Null(retrievedPortfolio);
    }

    [Fact]
    public async Task GetPortfolioAsync_ReturnsCorrectPortfolio()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolio = new Portfolio { Name = "Get Async Test Portfolio" };
        context.Portfolios.Add(portfolio);
        context.SaveChanges();

        var retrievedPortfolio = await repository.GetPortfolioAsync("Get Async Test Portfolio");
        Assert.NotNull(retrievedPortfolio);
        Assert.Equal(portfolio.Name, retrievedPortfolio!.Name);
    }

    [Fact]
    public async Task GetPortfolioAsync_ReturnsNullForNonExistentPortfolio()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var retrievedPortfolio = await repository.GetPortfolioAsync("Non Existent Async Portfolio");
        Assert.Null(retrievedPortfolio);
    }

    [Fact]
    public void AddPortfolios_AddsMultiplePortfoliosToDatabase()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>
        {
            new Portfolio { Name = "Portfolio 1" },
            new Portfolio { Name = "Portfolio 2" }
        };

        repository.AddPortfolios(portfolios);

        Assert.Equal(2, context.Portfolios.Count());
    }

    [Fact]
    public void AddPortfolios_DoesNotAddEmptyList()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>();

        repository.AddPortfolios(portfolios);

        Assert.Equal(0, context.Portfolios.Count());
    }

    [Fact]
    public async Task AddPortfoliosAsync_AddsMultiplePortfoliosToDatabase()
    {
        var context = CreateContext();
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
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>();

        await repository.AddPortfoliosAsync(portfolios);

        Assert.Equal(0, context.Portfolios.Count());
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsCorrectPortfolio()
    {
        var context = CreateContext();
        var repository = new PortfolioRepository(context);

        var portfolio = new Portfolio { Name = "Get By ID Portfolio" };
        context.Portfolios.Add(portfolio);
        context.SaveChanges();

        var retrievedPortfolio = await repository.GetPortfolioAsync(portfolio.Id);
        Assert.NotNull(retrievedPortfolio);
        Assert.Equal(portfolio.Name, retrievedPortfolio.Name);
    }

    [Fact]
    public async Task GetPortfoliosAsync_ByNames_ReturnsCorrectPortfolios()
    {
        var context = CreateContext();
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
}