using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;
namespace PerformanceApp.Infrastructure.Test.Repositories;

public class PortfolioValueRepositoryTest : BaseRepositoryTest
{
    private readonly PortfolioValueRepository _repository;

    public PortfolioValueRepositoryTest()
    {
        _repository = new PortfolioValueRepository(_context);
    }

    [Fact]
    public async Task GetPortfolioValuesAsync_ReturnsAllPortfolioValues()
    {
        // Arrange
        var expected = new PortfolioValueBuilder()
            .Many(20)
            .ToList();

        await _context.PortfolioValues.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetPortfolioValuesAsync();
        var actual = fetched.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.Value, a.Value);
            Assert.Equal(e.Bankday, a.Bankday);
        }
    }

    [Fact]
    public async Task GetPortfolioValuesAsync_ReturnsEmptyListWhenNoData()
    {
        // Act
        var result = await _repository.GetPortfolioValuesAsync();

        // Assert
        Assert.Empty(result);
    }
}