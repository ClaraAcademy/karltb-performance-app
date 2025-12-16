using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Test.Repositories.Position.Fixture;

namespace PerformanceApp.Infrastructure.Test.Repositories.Position;

public class PositionRepository_Bond_Tests : PositionRepositoryFixture
{
    [Fact]
    public async Task GetBondPositionsAsync_ReturnsFilteredPositions()
    {
        var portfolio = new PortfolioBuilder()
            .Build();
        var stock = new StockPositionBuilder()
            .WithId(1)
            .WithPortfolioNavigation(portfolio)
            .Build();
        var bond = new BondPositionBuilder()
            .WithId(2)
            .WithPortfolioNavigation(portfolio)
            .Build();
        var index = new IndexPositionBuilder()
            .WithId(3)
            .WithPortfolioNavigation(portfolio)
            .Build();
        var positions = new[] { stock, bond, index };
        var expected = bond;
        var bankday = bond.Bankday!.Value;
        // Arrange
        await _context.Positions.AddRangeAsync(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetBondPositionsAsync(bankday, portfolio.Id);

        // Assert
        Assert.NotNull(result);
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
    public async Task GetBondPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var position = new BondPositionBuilder()
            .WithId(1)
            .WithPortfolioId(1)
            .WithBankday(bankday)
            .Build();
        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetBondPositionsAsync(bankday.AddDays(1), 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var portfolioId = 5;
        var position = new BondPositionBuilder()
            .WithId(1)
            .WithPortfolioId(portfolioId)
            .WithBankday(bankday)
            .Build();

        await _context.Positions.AddAsync(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetBondPositionsAsync(bankday, portfolioId - 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = await _repository.GetBondPositionsAsync(bankday, 1);

        // Assert
        Assert.Empty(result);
    }

}