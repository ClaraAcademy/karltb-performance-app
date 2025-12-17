using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories.Positions;

public class PositionRepository_Index_Tests : BaseRepositoryTest
{
    private readonly IPositionRepository _repository;
    public PositionRepository_Index_Tests()
    {
        _repository = new PositionRepository(_context);
    }
    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var portfolio = new PortfolioBuilder()
            .Build();

        var stock = new StockPositionBuilder()
            .WithPortfolio(portfolio)
            .Build();
        var bond = new BondPositionBuilder()
            .WithPortfolio(portfolio)
            .Build();
        var index = new IndexPositionBuilder()
            .WithPortfolio(portfolio)
            .Build();

        var positions = new[] { stock, bond, index };
        var expected = index;
        var bankday = (DateOnly)index.Bankday!;

        await _context.Positions.AddRangeAsync(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetIndexPositionsAsync(bankday, portfolio.Id);

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