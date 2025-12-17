using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Test.Repositories.Positions;

public class PositionRepository_Stock_Tests : BaseRepositoryTest
{
    private readonly IPositionRepository _repository;
    public PositionRepository_Stock_Tests()
    {
        _repository = new PositionRepository(_context);
    }
    [Fact]
    public async Task GetStockPositionsAsync_ReturnsFilteredPositions()
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

        var positions = new [] { stock, bond, index };
        var expected = stock;
        var bankday = (DateOnly)stock.Bankday!;

        await _context.Positions.AddRangeAsync(positions);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetStockPositionsAsync(bankday, 1);

        // Assert
        Assert.Single(fetched);
        var actual = fetched.First();
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