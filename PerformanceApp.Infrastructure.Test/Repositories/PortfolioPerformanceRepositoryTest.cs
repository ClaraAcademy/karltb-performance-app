using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class PortfolioPerformanceRepositoryTest : BaseRepositoryTest
{
    private readonly PortfolioPerformanceRepository _repository;
    public PortfolioPerformanceRepositoryTest()
    {
        _repository = new PortfolioPerformanceRepository(_context);
    }

    [Fact]
    public async Task GetPortfolioPerformancesAsync_ReturnsAllPortfolioPerformances()
    {
        // Arrange
        var portfolio = new PortfolioBuilder()
            .WithId(1)
            .Build();
        var expected = new PortfolioPerformanceBuilder()
            .WithPortfolio(portfolio)
            .Many(5)
            .ToList();
        await _context
            .PortfolioPerformances
            .AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetPortfolioPerformancesAsync();
        var actual = result.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.TypeId, a.TypeId);
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.Value, a.Value);
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