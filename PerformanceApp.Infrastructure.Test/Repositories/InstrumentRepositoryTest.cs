using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class InstrumentRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentRepository _repository;

    public InstrumentRepositoryTest()
    {
        _repository = new InstrumentRepository(_context);
    }

    [Fact]
    public async Task AddInstrumentsAsync_AddsMultipleInstruments()
    {
        // Arrange
        var expected = new InstrumentBuilder()
            .Many(5)
            .ToList();

        // Act
        await _repository.AddInstrumentsAsync(expected);
        var actual = _context
            .Instruments
            .ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
            Assert.Equal(e.TypeId, a.TypeId);
        }
    }

    [Fact]
    public async Task GetInstrumentsAsync_ReturnsAllInstruments()
    {
        // Arrange
        var expected = new InstrumentBuilder()
            .Many(7)
            .ToList();

        _context.Instruments.AddRange(expected);
        _context.SaveChanges();

        // Act
        var actual = await _repository.GetInstrumentsAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
            Assert.Equal(e.TypeId, a.TypeId);
        }
    }

    [Fact]
    public async Task GetInstrumentsAsync_ReturnsEmptyListWhenNoInstruments()
    {
        // Act
        var actual = await _repository.GetInstrumentsAsync();

        // Assert
        Assert.Empty(actual);
    }
}