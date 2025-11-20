using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
namespace PerformanceApp.Data.Test.Repositories;

public class KeyFigureValueRepositoryTest
{
    [Fact]
    public async Task GetKeyFigureValuesAsync_ReturnsValues_ForGivenPortfolioId()
    {
        var context = BaseRepositoryTest.GetContext();

        var portfolios = new List<Portfolio>
        {
            new Portfolio { Id = 1, Name = "Portfolio 1" },
            new Portfolio { Id = 2, Name = "Portfolio 2" }
        };

        context.Portfolios.AddRange(portfolios);

        await context.SaveChangesAsync();

        var keyFigureValues = new List<KeyFigureValue>
        {
            new KeyFigureValue { KeyFigureId = 1, PortfolioId = 1, Value = 100m },
            new KeyFigureValue { KeyFigureId = 2, PortfolioId = 1, Value = 200m },
            new KeyFigureValue { KeyFigureId = 3, PortfolioId = 2, Value = 300m }
        };

        context.KeyFigureValues.AddRange(keyFigureValues);
        await context.SaveChangesAsync();

        var repository = new KeyFigureValueRepository(context);

        // Act
        var result = await repository.GetKeyFigureValuesAsync(1);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, kfv => Assert.Equal(1, kfv.PortfolioId));
    }

    [Fact]
    public async Task GetKeyFigureValuesAsync_ReturnsEmpty_ForNonExistingPortfolioId()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new KeyFigureValueRepository(context);

        // Act
        var result = await repository.GetKeyFigureValuesAsync(999);

        // Assert
        Assert.Empty(result);
    }
}