using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class InstrumentTypeRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentTypeRepository _repository;

    public InstrumentTypeRepositoryTest()
    {
        _repository = new InstrumentTypeRepository(_context);
    }

    private static List<InstrumentType> CreateInstrumentTypes()
    {
        return [
            new InstrumentType { Name = "Equity" },
            new InstrumentType { Name = "Bond" },
            new InstrumentType { Name = "Commodity" }
        ];
    }

    [Fact]
    public async Task AddInstrumentTypesAsync_AddsMultipleInstrumentTypesToDatabase()
    {
        var expected = CreateInstrumentTypes();

        await _repository.AddInstrumentTypesAsync(expected);

        var actual = await _context.InstrumentTypes.ToListAsync();
        Assert.Equal(expected.Count, actual.Count);
    }

    [Fact]
    public async Task AddInstrumentTypesAsync_DoesNotAddEmptyList()
    {
        var empty = new List<InstrumentType>();
        await _repository.AddInstrumentTypesAsync(empty);

        var actual = await _context.InstrumentTypes.ToListAsync();
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetInstrumentTypesAsync_ReturnsCorrectInstrumentTypes()
    {
        var instrumentTypes = CreateInstrumentTypes();
        _context.InstrumentTypes.AddRange(instrumentTypes);
        await _context.SaveChangesAsync();

        var actual = await _repository.GetInstrumentTypesAsync(["Bond", "Commodity"]);

        Assert.Equal(2, actual.Count);
        Assert.Contains(actual, it => it.Name == "Bond");
        Assert.Contains(actual, it => it.Name == "Commodity");
    }
}