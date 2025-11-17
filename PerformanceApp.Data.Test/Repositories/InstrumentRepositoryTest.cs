using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentRepositoryTest : RepositoryTest
{
    [Fact]
    public async Task AddInstrument_AddsInstrument()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instrument = new Instrument
        {
            Name = "Test Instrument",
            TypeId = 1
        };

        await repo.AddInstrumentAsync(instrument);

        var instrumentsInDb = context.Instruments.ToList();
        Assert.Single(instrumentsInDb);
        Assert.Equal("Test Instrument", instrumentsInDb[0].Name);
    }

    [Fact]
    public async Task AddInstrumentAsync_AddsInstrument()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instrument = new Instrument
        {
            Name = "Async Test Instrument",
            TypeId = 2
        };

        await repo.AddInstrumentAsync(instrument);

        var instrumentsInDb = context.Instruments.ToList();
        Assert.Single(instrumentsInDb);
        Assert.Equal("Async Test Instrument", instrumentsInDb[0].Name);
    }

    [Fact]
    public void AddInstruments_AddsMultipleInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Instrument 1", TypeId = 1 },
            new Instrument { Name = "Instrument 2", TypeId = 2 }
        };

        repo.AddInstruments(instruments);

        var instrumentsInDb = context.Instruments.ToList();
        Assert.Equal(2, instrumentsInDb.Count);
    }

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
    public void GetInstrument_ReturnsCorrectInstrument()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instrument = new Instrument
        {
            Name = "Get Test Instrument",
            TypeId = 1
        };
        context.Instruments.Add(instrument);
        context.SaveChanges();

        var retrievedInstrument = repo.GetInstrument("Get Test Instrument");

        Assert.NotNull(retrievedInstrument);
        Assert.Equal(instrument.Name, retrievedInstrument.Name);
    }

    [Fact]
    public void GetInstrument_ReturnsNullForNonExistentInstrument()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var retrievedInstrument = repo.GetInstrument("Non Existent Instrument");

        Assert.Null(retrievedInstrument);
    }

    [Fact]
    public async Task GetInstrumentAsync_ReturnsCorrectInstrument()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instrument = new Instrument
        {
            Name = "Async Get Test Instrument",
            TypeId = 1
        };
        context.Instruments.Add(instrument);
        context.SaveChanges();

        var retrievedInstrument = await repo.GetInstrumentAsync("Async Get Test Instrument");

        Assert.NotNull(retrievedInstrument);
        Assert.Equal(instrument.Name, retrievedInstrument.Name);
    }

    [Fact]
    public async Task GetInstrumentAsync_ReturnsNullForNonExistentInstrument()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var retrievedInstrument = await repo.GetInstrumentAsync("Non Existent Instrument");

        Assert.Null(retrievedInstrument);
    }

    [Fact]
    public void GetInstruments_ReturnsAllInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Instrument 1", TypeId = 1 },
            new Instrument { Name = "Instrument 2", TypeId = 2 }
        };
        context.Instruments.AddRange(instruments);
        context.SaveChanges();

        var retrievedInstruments = repo.GetInstruments();

        Assert.Equal(2, retrievedInstruments.Count);
        foreach (var instrument in instruments)
        {
            Assert.Contains(retrievedInstruments, i => i.Name == instrument.Name);
        }
    }

    [Fact]
    public void GetInstruments_ReturnsEmptyListWhenNoInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var retrievedInstruments = repo.GetInstruments();

        Assert.Empty(retrievedInstruments);
    }

    [Fact]
    public void GetInstruments_WithNames_ReturnsCorrectInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Instrument A", TypeId = 1 },
            new Instrument { Name = "Instrument B", TypeId = 2 },
            new Instrument { Name = "Instrument C", TypeId = 3 }
        };
        context.Instruments.AddRange(instruments);
        context.SaveChanges();

        var namesToRetrieve = new List<string> { "Instrument A", "Instrument C" };
        var retrievedInstruments = repo.GetInstruments(namesToRetrieve);

        Assert.Equal(2, retrievedInstruments.Count);
        Assert.Contains(retrievedInstruments, i => i.Name == "Instrument A");
        Assert.Contains(retrievedInstruments, i => i.Name == "Instrument C");
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

    [Fact]
    public async Task GetInstrumentsAsync_WithNames_ReturnsCorrectInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Async Instrument A", TypeId = 1 },
            new Instrument { Name = "Async Instrument B", TypeId = 2 },
            new Instrument { Name = "Async Instrument C", TypeId = 3 }
        };
        context.Instruments.AddRange(instruments);
        context.SaveChanges();

        var namesToRetrieve = new List<string> { "Async Instrument A", "Async Instrument C" };
        var retrievedInstruments = await repo.GetInstrumentsAsync(namesToRetrieve);

        Assert.Equal(2, retrievedInstruments.Count);
        Assert.Contains(retrievedInstruments, i => i.Name == "Async Instrument A");
        Assert.Contains(retrievedInstruments, i => i.Name == "Async Instrument C");
    }

    [Fact]
    public async Task GetInstrumentsAsync_WithNames_ReturnsEmptyListWhenNoMatch()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Instrument X", TypeId = 1 },
            new Instrument { Name = "Instrument Y", TypeId = 2 }
        };
        context.Instruments.AddRange(instruments);
        context.SaveChanges();

        var namesToRetrieve = new List<string> { "Non Existent Instrument" };
        var retrievedInstruments = await repo.GetInstrumentsAsync(namesToRetrieve);

        Assert.Empty(retrievedInstruments);
    }

    [Fact]
    public async Task GetInstrumentsAsync_WithNames_ReturnsEmptyListWhenNoInstruments()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var namesToRetrieve = new List<string> { "Any Instrument" };
        var retrievedInstruments = await repo.GetInstrumentsAsync(namesToRetrieve);

        Assert.Empty(retrievedInstruments);
    }

    [Fact]
    public async Task GetInstrumentsAsync_WithNames_ReturnsEmptyListWhenNamesListIsEmpty()
    {
        var context = CreateContext();
        var repo = new InstrumentRepository(context);

        var instruments = new List<Instrument>
        {
            new Instrument { Name = "Instrument 1", TypeId = 1 },
            new Instrument { Name = "Instrument 2", TypeId = 2 }
        };
        context.Instruments.AddRange(instruments);
        context.SaveChanges();

        var namesToRetrieve = new List<string>();
        var retrievedInstruments = await repo.GetInstrumentsAsync(namesToRetrieve);

        Assert.Empty(retrievedInstruments);
    }




}