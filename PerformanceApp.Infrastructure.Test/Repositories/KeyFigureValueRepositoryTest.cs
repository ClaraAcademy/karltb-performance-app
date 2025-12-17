using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
namespace PerformanceApp.Infrastructure.Test.Repositories;

public class KeyFigureValueRepositoryTest : BaseRepositoryTest
{
    private readonly KeyFigureValueRepository _repository;

    public KeyFigureValueRepositoryTest()
    {
        _repository = new KeyFigureValueRepository(_context);
    }

    [Fact]
    public async Task GetKeyFigureValuesAsync_ReturnsValues_ForGivenPortfolioId()
    {
        // Arrange
        var portfolio = new PortfolioBuilder()
            .Build();
        var expected = new KeyFigureValueBuilder()
            .WithPortfolio(portfolio)
            .Many(10)
            .ToList();
        await _context
            .KeyFigureValues
            .AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var portfolioId = portfolio.Id;
        var retrieved = await _repository.GetKeyFigureValuesAsync(portfolioId);
        var actual = retrieved.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.KeyFigureId, a.KeyFigureId);
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.Value, a.Value);
        }
    }

    [Fact]
    public async Task GetKeyFigureValuesAsync_ReturnsEmpty_ForNonExistingPortfolioId()
    {
        // Act
        var result = await _repository.GetKeyFigureValuesAsync(999);

        // Assert
        Assert.Empty(result);
    }
}