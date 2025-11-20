using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioPerformanceRepositoryTest
{
    [Fact]
    public async Task GetPortfolioPerformancesAsync_ReturnsAllPortfolioPerformances()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioPerformanceRepository(context);

        var portfolioPerformances = new List<PortfolioPerformance>
        {
            new PortfolioPerformance { TypeId = 1, PortfolioId = 1, Value = 150m },
            new PortfolioPerformance { TypeId = 2, PortfolioId = 2, Value = 250m }
        };

        context.PortfolioPerformances.AddRange(portfolioPerformances);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetPortfolioPerformancesAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, pp => pp.PortfolioId == 1 && pp.Value == 150m);
        Assert.Contains(result, pp => pp.PortfolioId == 2 && pp.Value == 250m);
    }

    [Fact]
    public async Task GetPortfolioPerformancesAsync_ReturnsEmpty_WhenNoPerformancesExist()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioPerformanceRepository(context);

        // Act
        var result = await repository.GetPortfolioPerformancesAsync();

        // Assert
        Assert.Empty(result);
    }
}