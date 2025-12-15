using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
namespace PerformanceApp.Data.Test.Repositories;

public class PortfolioValueRepositoryTest : BaseRepositoryTest
{
    private readonly PortfolioValueRepository _repository;

    public PortfolioValueRepositoryTest()
    {
        _repository = new PortfolioValueRepository(_context);
    }
    private static int GetId(int i) => i;
    private static decimal GetValue(int i) => i * 1000m;
    private static PortfolioValue CreatePortfolioValue(int i) => new() { PortfolioId = GetId(i), Value = GetValue(i) };
    private static List<PortfolioValue> CreatePortfolioValues(int count)
    {
        return Enumerable.Range(1, count)
            .Select(CreatePortfolioValue)
            .ToList();
    }

    [Fact]
    public async Task GetPortfolioValuesAsync_ReturnsAllPortfolioValues()
    {
        // Arrange
        var nExpected = 54;
        var portfolioValues = CreatePortfolioValues(nExpected);

        await _context.PortfolioValues.AddRangeAsync(portfolioValues);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetPortfolioValuesAsync();

        // Assert
        var nActual = fetched.Count();
        Assert.Equal(nExpected, nActual);
        for (int i = 1; i <= nExpected; i++)
        {
            Assert.Contains(fetched, pv => pv.PortfolioId == GetId(i) && pv.Value == GetValue(i));
        }
    }

    [Fact]
    public async Task GetPortfolioValuesAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new PortfolioValueRepository(context);

        // Act
        var result = await repository.GetPortfolioValuesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}