using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentTypeRepositoryTest : RepositoryTest
{
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
}