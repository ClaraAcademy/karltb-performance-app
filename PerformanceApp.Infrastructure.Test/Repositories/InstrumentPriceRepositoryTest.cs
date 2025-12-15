using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class InstrumentPriceRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentPriceRepository _repository;

    public InstrumentPriceRepositoryTest()
    {
        _repository = new InstrumentPriceRepository(_context);
    }

    private static List<InstrumentPrice> CreateInstrumentPrices()
    {
        return [
            new InstrumentPrice { InstrumentId = 1, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 100m },
            new InstrumentPrice { InstrumentId = 2, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 200m }
        ];
    }

    [Fact]
    public void AddInstrumentPrices_AddsInstrumentPricesToDatabase()
    {
        var expected = CreateInstrumentPrices();

        _repository.AddInstrumentPrices(expected);

        foreach (var instrumentPrice in expected)
        {
            var actual = _context.InstrumentPrices.Find(instrumentPrice.InstrumentId, instrumentPrice.Bankday);
            Assert.NotNull(actual);
            Assert.Equal(instrumentPrice.Price, actual.Price);
        }
    }

    [Fact]
    public void AddInstrumentPrices_DoesNotAddEmptyList()
    {
        var empty = new List<InstrumentPrice>();

        _repository.AddInstrumentPrices(empty);

        Assert.Empty(_context.InstrumentPrices.ToList());
    }

    [Fact]
    public async Task AddInstrumentPricesAsync_AddsInstrumentPricesToDatabase()
    {
        var expected = CreateInstrumentPrices();

        await _repository.AddInstrumentPricesAsync(expected);

        foreach (var instrumentPrice in expected)
        {
            var actual = await _context.InstrumentPrices.FindAsync(instrumentPrice.InstrumentId, instrumentPrice.Bankday);
            Assert.NotNull(actual);
            Assert.Equal(instrumentPrice.Price, actual.Price);
        }
    }

    [Fact]
    public async Task AddInstrumentPricesAsync_DoesNotAddEmptyList()
    {
        var empty = new List<InstrumentPrice>();
        await _repository.AddInstrumentPricesAsync(empty);

        Assert.Empty(_context.InstrumentPrices.ToList());
    }

    [Fact]
    public async Task GetInstrumentPricesAsync_ReturnsAllInstrumentPrices()
    {
        var expected = CreateInstrumentPrices();

        _context.InstrumentPrices.AddRange(expected);
        _context.SaveChanges();

        var retrieved = await _repository.GetInstrumentPricesAsync();

        Assert.Equal(expected.Count, retrieved.Count());

        foreach (var instrumentPrice in expected)
        {
            Assert.Contains(retrieved, ip => ip.InstrumentId == instrumentPrice.InstrumentId && ip.Bankday == instrumentPrice.Bankday && ip.Price == instrumentPrice.Price);
        }

    }
}