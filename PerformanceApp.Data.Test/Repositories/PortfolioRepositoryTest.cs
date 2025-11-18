using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Xunit;

namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioRepositoryTest 
{

    [Fact]
    public async Task AddPortfoliosAsync_AddsMultiplePortfoliosToDatabase()
    {
        var context = RepositoryTest.GetContext();
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
        var context = RepositoryTest.GetContext();
        var repository = new PortfolioRepository(context);

        var portfolios = new List<Portfolio>();

        await repository.AddPortfoliosAsync(portfolios);

        Assert.Equal(0, context.Portfolios.Count());
    }

    [Fact]
    public async Task GetPortfolioAsync_ById_ReturnsCorrectPortfolio()
    {
        var context = RepositoryTest.GetContext();
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
        var context = RepositoryTest.GetContext();
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