using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    [Fact]
    public async Task Seed_AddsInstruments()
    {
        // Arrange
        var expected = InstrumentData.Instruments;

        // Act
        await Seed();

        var instruments = await _context.Instruments.ToListAsync();
        var actual = instruments
            .Select(i => i.Name)
            .OrderBy(n => n)
            .ToList();

        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected, actual!);
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        await Seed();
        var initialCount = await _context.Instruments.CountAsync();

        // Act
        await Seed();

        // Assert
        var finalCount = await _context.Instruments.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}