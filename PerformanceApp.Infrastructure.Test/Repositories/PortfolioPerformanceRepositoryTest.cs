using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class PortfolioPerformanceRepositoryTest : BaseRepositoryTest
{
    private readonly PortfolioPerformanceRepository _repository;
    public PortfolioPerformanceRepositoryTest()
    {
        _repository = new PortfolioPerformanceRepository(_context);
    }

    private static PortfolioPerformance CreatePortfolioPerformance(int i) => new() { TypeId = i, PortfolioId = i, Value = 100m + i };
    private static List<PortfolioPerformance> CreatePortfolioPerformances(int count)
    {
        return Enumerable.Range(1, count)
            .Select(CreatePortfolioPerformance)
            .ToList();
    }

    [Fact]
    public async Task GetPortfolioPerformancesAsync_ReturnsAllPortfolioPerformances()
    {
        // Arrange
        var n = 40;
        var portfolioPerformances = CreatePortfolioPerformances(n);
        _context.PortfolioPerformances.AddRange(portfolioPerformances);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPortfolioPerformancesAsync();

        // Assert
        Assert.Equal(n, result.Count());
        for (int i = 1; i <= n; i++)
        {
            Assert.Contains(result, pp => pp.TypeId == i && pp.Value == 100m + i);
        }
    }

    [Fact]
    public async Task GetPortfolioPerformancesAsync_ReturnsEmpty_WhenNoPerformancesExist()
    {
        // Act
        var result = await _repository.GetPortfolioPerformancesAsync();

        // Assert
        Assert.Empty(result);
    }
}