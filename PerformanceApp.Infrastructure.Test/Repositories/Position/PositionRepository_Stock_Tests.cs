using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Test.Repositories.Position.Fixture;

namespace PerformanceApp.Infrastructure.Test.Repositories.Position;

public class PositionRepository_Stock_Tests : PositionRepositoryFixture
{
    [Fact]
    public async Task GetStockPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
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
        var expected = stock;
        var bankday = stock.Bankday!.Value;

        await _context.Positions.AddRangeAsync(positions);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetStockPositionsAsync(bankday, 1);
        var actual = fetched.First();

        // Assert
        Assert.Single(fetched);
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
    public async Task GetStockPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var position = new StockPositionBuilder()
            .WithPortfolioId(1)
            .Build();
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetStockPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var id = 6;
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var position = new StockPositionBuilder()
            .WithPortfolioId(id)
            .WithBankday(bankday)
            .Build();
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetStockPositionsAsync(bankday, id - 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = await _repository.GetStockPositionsAsync(bankday, 1);

        // Assert
        Assert.Empty(result);
    }

}