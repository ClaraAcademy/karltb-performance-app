using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentRepositoryTest : RepositoryTest
{

    [Fact]
    public async Task AddInstrumentsAsync_AddsMultipleInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Async Instrument 1", TypeId = 1 },
            new Instrument { Name = "Async Instrument 2", TypeId = 2 }
        };

        await repo.AddInstrumentsAsync(instruments);

        var instrumentsInDb = context.Instruments.ToList();
        Assert.Equal(2, instrumentsInDb.Count);
    }

    [Fact]
    public async Task GetInstrumentsAsync_ReturnsAllInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Async Instrument 1", TypeId = 1 },
            new Instrument { Name = "Async Instrument 2", TypeId = 2 }
        };
        context.Instruments.AddRange(instruments);
        context.SaveChanges();

        var retrievedInstruments = await repo.GetInstrumentsAsync();

        Assert.Equal(2, retrievedInstruments.Count);
        foreach (var instrument in instruments)
        {
            Assert.Contains(retrievedInstruments, i => i.Name == instrument.Name);
        }
    }

    [Fact]
    public async Task GetInstrumentsAsync_ReturnsEmptyListWhenNoInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var retrievedInstruments = await repo.GetInstrumentsAsync();

        Assert.Empty(retrievedInstruments);
    }
}