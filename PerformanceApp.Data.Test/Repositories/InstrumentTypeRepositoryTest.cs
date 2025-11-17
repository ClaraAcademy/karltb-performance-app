using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentTypeRepositoryTest : RepositoryTest
{
    [Fact]
    public void AddInstrumentType_AddsInstrumentTypeToDatabase()
    {
        var context = CreateContext();
        var repository = new InstrumentTypeRepository(context);

        var instrumentType = new InstrumentType { Name = "Equity" };
        repository.AddInstrumentType(instrumentType);

        var retrievedInstrumentType = context.InstrumentTypes.Single(it => it.Name == "Equity");
        Assert.Equal(instrumentType.Name, retrievedInstrumentType.Name);
    }

    [Fact]
    public async Task AddInstrumentTypeAsync_AddsInstrumentTypeToDatabase()
    {
        var context = CreateContext();
        var repository = new InstrumentTypeRepository(context);

        var instrumentType = new InstrumentType { Name = "Bond" };
        await repository.AddInstrumentTypeAsync(instrumentType);

        var retrievedInstrumentType = await context.InstrumentTypes.SingleAsync(it => it.Name == "Bond");
        Assert.Equal(instrumentType.Name, retrievedInstrumentType.Name);
    }

    [Fact]
    public void AddInstrumentTypes_AddsMultipleInstrumentTypesToDatabase()
    {
        var context = CreateContext();
        var repository = new InstrumentTypeRepository(context);

        var instrumentTypes = new List<InstrumentType>
        {
            new InstrumentType { Name = "Equity" },
            new InstrumentType { Name = "Bond" }
        };
        repository.AddInstrumentTypes(instrumentTypes);

        var retrievedInstrumentTypes = context.InstrumentTypes.ToList();
        Assert.Equal(2, retrievedInstrumentTypes.Count);
    }

    [Fact]
    public void AddInstrumentTypes_DoesNotAddEmptyList()
    {
        var context = CreateContext();
        var repository = new InstrumentTypeRepository(context);

        var instrumentTypes = new List<InstrumentType>();
        repository.AddInstrumentTypes(instrumentTypes);

        var retrievedInstrumentTypes = context.InstrumentTypes.ToList();
        Assert.Empty(retrievedInstrumentTypes);
    }   

    [Fact]
    public async Task AddInstrumentTypesAsync_AddsMultipleInstrumentTypesToDatabase()
    {
        var context = CreateContext();
        var repository = new InstrumentTypeRepository(context);

        var instrumentTypes = new List<InstrumentType>
        {
            new InstrumentType { Name = "Equity" },
            new InstrumentType { Name = "Bond" }
        };
        await repository.AddInstrumentTypesAsync(instrumentTypes);

        var retrievedInstrumentTypes = await context.InstrumentTypes.ToListAsync();
        Assert.Equal(2, retrievedInstrumentTypes.Count);
    }

    [Fact]
    public async Task AddInstrumentTypesAsync_DoesNotAddEmptyList()
    {
        var context = CreateContext();
        var repository = new InstrumentTypeRepository(context);

        var instrumentTypes = new List<InstrumentType>();
        await repository.AddInstrumentTypesAsync(instrumentTypes);

        var retrievedInstrumentTypes = await context.InstrumentTypes.ToListAsync();
        Assert.Empty(retrievedInstrumentTypes);
    }

    [Fact]
    public void GetInstrumentType_ReturnsCorrectInstrumentType()
    {
        var context = CreateContext();
        context.InstrumentTypes.Add(new InstrumentType { Name = "Equity" });
        context.SaveChanges();

        var repository = new InstrumentTypeRepository(context);
        var instrumentType = repository.GetInstrumentType("Equity");

        Assert.NotNull(instrumentType);
        Assert.Equal("Equity", instrumentType.Name);
    }

    [Fact]
    public async Task GetInstrumentTypeAsync_ReturnsCorrectInstrumentType()
    {
        var context = CreateContext();
        context.InstrumentTypes.Add(new InstrumentType { Name = "Bond" });
        await context.SaveChangesAsync();

        var repository = new InstrumentTypeRepository(context);
        var instrumentType = await repository.GetInstrumentTypeAsync("Bond");

        Assert.NotNull(instrumentType);
        Assert.Equal("Bond", instrumentType.Name);
    }

    [Fact]
    public void GetInstrumentTypes_ReturnsCorrectInstrumentTypes()
    {
        var context = CreateContext();
        context.InstrumentTypes.AddRange(new List<InstrumentType>
        {
            new InstrumentType { Name = "Equity" },
            new InstrumentType { Name = "Bond" },
            new InstrumentType { Name = "Commodity" }
        });
        context.SaveChanges();

        var repository = new InstrumentTypeRepository(context);
        var instrumentTypes = repository.GetInstrumentTypes(new List<string> { "Equity", "Commodity" });

        Assert.Equal(2, instrumentTypes.Count);
        Assert.Contains(instrumentTypes, it => it.Name == "Equity");
        Assert.Contains(instrumentTypes, it => it.Name == "Commodity");
    }

    [Fact]
    public async Task GetInstrumentTypesAsync_ReturnsCorrectInstrumentTypes()
    {
        var context = CreateContext();
        context.InstrumentTypes.AddRange(new List<InstrumentType>
        {
            new InstrumentType { Name = "Equity" },
            new InstrumentType { Name = "Bond" },
            new InstrumentType { Name = "Commodity" }
        });
        await context.SaveChangesAsync();

        var repository = new InstrumentTypeRepository(context);
        var instrumentTypes = await repository.GetInstrumentTypesAsync(new List<string> { "Bond", "Commodity" });

        Assert.Equal(2, instrumentTypes.Count);
        Assert.Contains(instrumentTypes, it => it.Name == "Bond");
        Assert.Contains(instrumentTypes, it => it.Name == "Commodity");
    }

    [Fact]
    public void GetInstrumentTypes_ReturnsAllInstrumentTypes()
    {
        var context = CreateContext();
        context.InstrumentTypes.AddRange(new List<InstrumentType>
        {
            new InstrumentType { Name = "Equity" },
            new InstrumentType { Name = "Bond" }
        });
        context.SaveChanges();

        var repository = new InstrumentTypeRepository(context);
        var instrumentTypes = repository.GetInstrumentTypes();

        Assert.Equal(2, instrumentTypes.Count());
    }
}