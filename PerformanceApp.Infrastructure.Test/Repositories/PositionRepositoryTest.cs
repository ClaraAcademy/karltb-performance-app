using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class PositionRepositoryTest : BaseRepositoryTest
{
    private readonly PositionRepository _repository;

    public PositionRepositoryTest()
    {
        _repository = new PositionRepository(_context);
    }

    [Fact]
    public async Task GetPositionsAsync_ReturnsAllPositions()
    {
        // Arrange
        var expected = new PositionBuilder()
            .Many(9)
            .ToList();
        await _context.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetPositionsAsync();
        var actual = fetched.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.Count, a.Count);
            Assert.Equal(e.Amount, a.Amount);
            Assert.Equal(e.Proportion, a.Proportion);
            Assert.Equal(e.Nominal, a.Nominal);
        }
    }

    [Fact]
    public async Task GetPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Act
        var actual = await _repository.GetPositionsAsync();

        // Assert
        Assert.Empty(actual);
    }
}