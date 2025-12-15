using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class InstrumentRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentRepository _repository;

    public InstrumentRepositoryTest()
    {
        _repository = new InstrumentRepository(_context);
    }

    private static List<Instrument> CreateInstruments()
    {
        return [
            new Instrument { Name = "Instrument 1", TypeId = 1 },
            new Instrument { Name = "Instrument 2", TypeId = 2 }
        ];
    }

    [Fact]
    public async Task AddInstrumentsAsync_AddsMultipleInstruments()
    {
        var expected = CreateInstruments();

        await _repository.AddInstrumentsAsync(expected);

        var actual = _context.Instruments.ToList();
        Assert.Equal(expected.Count, actual.Count);
    }

    [Fact]
    public async Task GetInstrumentsAsync_ReturnsAllInstruments()
    {
        var expected = CreateInstruments();
        _context.Instruments.AddRange(expected);
        _context.SaveChanges();

        var actual = await _repository.GetInstrumentsAsync();

        Assert.Equal(expected.Count, actual.Count);
        foreach (var instrument in actual)
        {
            Assert.Contains(actual, i => i.Name == instrument.Name);
        }
    }

    [Fact]
    public async Task GetInstrumentsAsync_ReturnsEmptyListWhenNoInstruments()
    {
        var actual = await _repository.GetInstrumentsAsync();

        Assert.Empty(actual);
    }
}