using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Builders;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class InstrumentPerformanceRepositoryTest : BaseRepositoryTest
{
    private readonly InstrumentPerformanceRepository _repository;

    public InstrumentPerformanceRepositoryTest()
    {
        _repository = new InstrumentPerformanceRepository(_context);
    }

    [Fact]
    public async Task GetInstrumentPerformancesAsync_ReturnsPerformances()
    {
        // Arrange
        var expected = new InstrumentPerformanceBuilder()
            .Many(5)
            .ToList();

        await _context.InstrumentPerformances.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _repository.GetInstrumentPerformancesAsync();

        // Assert
        Assert.Equal(expected.Count(), actual.Count());
        for (int i = 0; i < expected.Count; i++)
        {
            var e = expected[i];
            var a = actual.ElementAt(i);

            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.TypeId, a.TypeId);
            Assert.Equal(e.PeriodStart, a.PeriodStart);
            Assert.Equal(e.PeriodEnd, a.PeriodEnd);
            Assert.Equal(e.Value, a.Value);
        }
    }

    [Fact]
    public async Task AddInstrumentPerformancesAsync_AddsPerformances()
    {
        // Arrange
        var instrument = new InstrumentBuilder()
            .Build();
        var expected = new InstrumentPerformanceBuilder()
            .WithInstrumentNavigation(instrument)
            .Many(4)
            .ToList();

        // Act
        await _repository.AddInstrumentPerformancesAsync(expected);
        var actual = await _context.InstrumentPerformances.ToListAsync();

        // Assert
        Assert.Equal(expected.Count(), actual.Count());
        for (int i = 0; i < expected.Count(); i++)
        {
            var e = expected[i];
            var a = actual[i];

            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.TypeId, a.TypeId);
            Assert.Equal(e.PeriodStart, a.PeriodStart);
            Assert.Equal(e.PeriodEnd, a.PeriodEnd);
            Assert.Equal(e.Value, a.Value);
        }
    }
}