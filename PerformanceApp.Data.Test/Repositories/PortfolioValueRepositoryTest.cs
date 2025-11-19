using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioValueRepositoryTest
{
    [Fact]
    public async Task GetPortfolioValuesAsync_ReturnsAllPortfolioValues()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        context.PortfolioValues.AddRange(
            new PortfolioValue { PortfolioId = 1, Value = 1000m },
            new PortfolioValue { PortfolioId = 2, Value = 2000m }
        );
        await context.SaveChangesAsync();

        var repository = new PortfolioValueRepository(context);

        // Act
        var result = await repository.GetPortfolioValuesAsync();

        // Assert
        Assert.NotNull(result);
        var portfolioValues = result.ToList();
        Assert.Equal(2, portfolioValues.Count);
        Assert.Contains(portfolioValues, pv => pv.PortfolioId == 1 && pv.Value == 1000m);
        Assert.Contains(portfolioValues, pv => pv.PortfolioId == 2 && pv.Value == 2000m);
    }

    [Fact]
    public async Task GetPortfolioValuesAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var context = RepositoryTest.GetContext();
        var repository = new PortfolioValueRepository(context);

        // Act
        var result = await repository.GetPortfolioValuesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}