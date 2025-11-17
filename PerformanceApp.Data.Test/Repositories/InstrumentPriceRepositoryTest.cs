using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentPriceRepositoryTest : RepositoryTest
{
    [Fact]
    public void AddInstrumentPrices_AddsInstrumentPricesToDatabase()
    {
        var context = CreateContext();
        var repository = new InstrumentPriceRepository(context);

        var instrumentPrices = new List<InstrumentPrice>
        {
            new InstrumentPrice { InstrumentId = 3, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 300m },
            new InstrumentPrice { InstrumentId = 4, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 400m }
        };

        repository.AddInstrumentPrices(instrumentPrices);

        foreach (var instrumentPrice in instrumentPrices)
        {
            var retrievedInstrumentPrice = context.InstrumentPrices.Find(instrumentPrice.InstrumentId, instrumentPrice.Bankday);
            Assert.NotNull(retrievedInstrumentPrice);
            Assert.Equal(instrumentPrice.Price, retrievedInstrumentPrice.Price);
        }
    }

    [Fact]
    public void AddInstrumentPrices_DoesNotAddEmptyList()
    {
        var context = CreateContext();
        var repository = new InstrumentPriceRepository(context);

        var instrumentPrices = new List<InstrumentPrice>();

        repository.AddInstrumentPrices(instrumentPrices);

        Assert.Empty(context.InstrumentPrices.ToList());
    }

    [Fact]
    public async Task AddInstrumentPricesAsync_AddsInstrumentPricesToDatabase()
    {
        var context = CreateContext();
        var repository = new InstrumentPriceRepository(context);

        var instrumentPrices = new List<InstrumentPrice>
        {
            new InstrumentPrice { InstrumentId = 5, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 500m },
            new InstrumentPrice { InstrumentId = 6, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 600m }
        };

        await repository.AddInstrumentPricesAsync(instrumentPrices);

        foreach (var instrumentPrice in instrumentPrices)
        {
            var retrievedInstrumentPrice = await context.InstrumentPrices.FindAsync(instrumentPrice.InstrumentId, instrumentPrice.Bankday);
            Assert.NotNull(retrievedInstrumentPrice);
            Assert.Equal(instrumentPrice.Price, retrievedInstrumentPrice.Price);
        }
    }

    [Fact]
    public async Task AddInstrumentPricesAsync_DoesNotAddEmptyList()
    {
        var context = CreateContext();
        var repository = new InstrumentPriceRepository(context);

        var instrumentPrices = new List<InstrumentPrice>();

        await repository.AddInstrumentPricesAsync(instrumentPrices);

        Assert.Empty(context.InstrumentPrices.ToList());
    }

    [Fact]
    public async Task GetInstrumentPricesAsync_ReturnsAllInstrumentPrices()
    {
        var context = CreateContext();
        var repository = new InstrumentPriceRepository(context);

        var instrumentPrices = new List<InstrumentPrice>
        {
            new InstrumentPrice { InstrumentId = 7, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 700m },
            new InstrumentPrice { InstrumentId = 8, Bankday = DateOnly.FromDateTime(DateTime.Now), Price = 800m }
        };

        context.InstrumentPrices.AddRange(instrumentPrices);
        context.SaveChanges();

        var retrievedInstrumentPrices = await repository.GetInstrumentPricesAsync();

        Assert.Equal(2, retrievedInstrumentPrices.Count());

        foreach (var instrumentPrice in instrumentPrices)
        {
            Assert.Contains(retrievedInstrumentPrices, ip => ip.InstrumentId == instrumentPrice.InstrumentId && ip.Bankday == instrumentPrice.Bankday && ip.Price == instrumentPrice.Price);
        }

    }
}