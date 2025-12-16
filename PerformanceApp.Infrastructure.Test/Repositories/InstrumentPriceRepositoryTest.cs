using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class InstrumentPriceRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentPriceRepository _repository;

    public InstrumentPriceRepositoryTest()
    {
        _repository = new InstrumentPriceRepository(_context);
    }

    [Fact]
    public async Task AddInstrumentPricesAsync_AddsInstrumentPricesToDatabase()
    {
        // Arrange
        var expected = new InstrumentPriceBuilder()
            .Many(9)
            .ToList();

        // Act
        await _repository.AddInstrumentPricesAsync(expected);
        var actual = await _context
            .InstrumentPrices
            .ToListAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(e.Price, a.Price);
        }
    }

    [Fact]
    public async Task AddInstrumentPricesAsync_DoesNotAddEmptyList()
    {
        // Arrange
        var empty = new List<InstrumentPrice>();

        // Act
        await _repository.AddInstrumentPricesAsync(empty);
        var actual = await _context
            .InstrumentPrices
            .ToListAsync();

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetInstrumentPricesAsync_ReturnsAllInstrumentPrices()
    {
        // Arrange
        var expected = new InstrumentPriceBuilder()
            .Many(6)
            .ToList();

        await _context
            .InstrumentPrices
            .AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var retrieved = await _repository.GetInstrumentPricesAsync();
        var actual = retrieved.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(e.Price, a.Price);
        }
    }
}