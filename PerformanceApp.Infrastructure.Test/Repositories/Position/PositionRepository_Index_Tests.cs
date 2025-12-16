using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Test.Repositories.Position.Fixture;

namespace PerformanceApp.Infrastructure.Test.Repositories.Position;

public class PositionRepository_Index_Tests : PositionRepositoryFixture
{
    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsFilteredPositions()
    {
        var stock = new StockPositionBuilder()
            .WithId(1)
            .WithPortfolioId(1)
            .Build();
        var bond = new BondPositionBuilder()
            .WithId(2)
            .WithPortfolioId(1)
            .Build();
        var index = new IndexPositionBuilder()
            .WithId(3)
            .WithPortfolioId(1)
            .Build();
        var positions = new[] { stock, bond, index };
        var expected = index;
        var bankday = index.Bankday!.Value;
        // Arrange
        await _context.Positions.AddRangeAsync(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetIndexPositionsAsync(bankday, 1);

        // Assert
        Assert.Single(result);
        var actual = result.First();
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.PortfolioId, actual.PortfolioId);
        Assert.Equal(expected.Bankday, actual.Bankday);
        Assert.Equal(expected.InstrumentId, actual.InstrumentId);
        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected.Amount, actual.Amount);
        Assert.Equal(expected.Proportion, actual.Proportion);
        Assert.Equal(expected.Nominal, actual.Nominal);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var position = new IndexPositionBuilder()
            .WithPortfolioId(1)
            .WithBankday(bankday)
            .Build();

        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetIndexPositionsAsync(bankday.AddDays(1), 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var portfolioId = 8;
        var position = new IndexPositionBuilder()
            .WithPortfolioId(portfolioId)
            .WithBankday(bankday)
            .Build();
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetIndexPositionsAsync(bankday, portfolioId - 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = await _repository.GetIndexPositionsAsync(bankday, 1);

        // Assert
        Assert.Empty(result);
    }

}