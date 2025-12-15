using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class PositionRepositoryTest : BaseRepositoryTest
{
    private readonly PositionRepository _repository;

    public PositionRepositoryTest()
    {
        _repository = new PositionRepository(_context);
    }

    private static Position CreatePosition(int id, int portfolioId, DateOnly bankday, string instrumentType)
    {
        return new Position
        {
            Id = id,
            PortfolioId = portfolioId,
            Bankday = bankday,
            InstrumentNavigation = new Instrument
            {
                InstrumentTypeNavigation = new InstrumentType { Name = instrumentType }
            }
        };
    }
    private static List<Position> CreatePositions()
    {
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        return [
            CreatePosition(1, 1, bankday, "Stock"),
            CreatePosition(2, 1, bankday, "Bond"),
            CreatePosition(3, 1, bankday, "Index"),
            CreatePosition(4, 2, bankday, "Stock")
        ];
    }

    private static DateOnly GetBankday() => DateOnly.FromDateTime(DateTime.Now);

    [Fact]
    public async Task GetPositionsAsync_ReturnsAllPositions()
    {
        // Arrange
        var positions = CreatePositions();
        var nExpected = positions.Count;
        await _context.AddRangeAsync(positions);
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetPositionsAsync();

        // Assert
        var nActual = fetched.Count();
        Assert.Equal(nExpected, nActual);
        foreach ((var e, var a) in positions.Zip(fetched))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.Bankday, a.Bankday);

            var eInstrumentType = e.InstrumentNavigation?.InstrumentTypeNavigation;
            var aInstrumentType = a.InstrumentNavigation?.InstrumentTypeNavigation;
            Assert.Equal(eInstrumentType?.Name, aInstrumentType?.Name);
        }
    }

    [Fact]
    public async Task GetPositionsAsync_ReturnsEmptyListWhenNoData()
    {
        // Act
        var result = await _repository.GetPositionsAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var bankday = GetBankday();
        var expectedId = 1;
        _context.Positions.AddRange(
            new Position
            {
                Id = expectedId,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Stock" }
                }
            },
            new Position
            {
                Id = 2,
                PortfolioId = 1,
                Bankday = bankday,
                InstrumentNavigation = new Instrument
                {
                    InstrumentTypeNavigation = new InstrumentType { Name = "Bond" }
                }
            }
        );
        await _context.SaveChangesAsync();

        // Act
        var fetched = await _repository.GetStockPositionsAsync(bankday, 1);

        // Assert
        Assert.Single(fetched);
        var actualId = fetched.First().Id;
        Assert.Equal(expectedId, actualId);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var bankday = GetBankday();
        var position = CreatePosition(2, 2, bankday, "Stock");
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetStockPositionsAsync(bankday.AddDays(1), 1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsEmptyListOnInvalidPortfolioId()
    {
        // Arrange
        var bankday = GetBankday();
        var portfolioId = 5;
        var position = CreatePosition(5, portfolioId, bankday, "Stock");
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetStockPositionsAsync(bankday, portfolioId - 1);

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

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var positions = CreatePositions();
        _context.Positions.AddRange(positions);
        await _context.SaveChangesAsync();

        var repository = new PositionRepository(_context);

        // Act
        var result = await repository.GetBondPositionsAsync(bankday, 1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(1, result.First().PortfolioId);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var position = CreatePosition(2, 1, bankday, "Bond");
        _context.Positions.Add(position);
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
        var position = CreatePosition(1, portfolioId, bankday, "Bond");
        _context.Positions.Add(position);
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

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsFilteredPositions()
    {
        // Arrange
        var positions = CreatePositions();
        _context.Positions.AddRange(positions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetIndexPositionsAsync(GetBankday(), 1);

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result.First().PortfolioId);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsEmptyListOnInvalidBankday()
    {
        // Arrange
        var bankday = DateOnly.FromDateTime(DateTime.Now);
        var position = CreatePosition(1, 1, bankday, "Index");
        _context.Positions.Add(position);
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
        var position = CreatePosition(1, portfolioId, bankday, "Index");
        _context.Positions.Add(position);
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