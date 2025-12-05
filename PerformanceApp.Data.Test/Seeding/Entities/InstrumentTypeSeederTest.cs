using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentTypeSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    [Fact]
    public async Task Seed_AddsInstrumentTypes()
    {
        // Arrange
        var expected = InstrumentTypeData.InstrumentTypes;

        // Act
        await Seed();

        var instrumentTypes = await _context.InstrumentTypes.ToListAsync();
        var actual = instrumentTypes
            .Select(it => it.Name)
            .OrderBy(n => n)
            .ToList();

        // Assert
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        await Seed();
        var initialCount = await _context.InstrumentTypes.CountAsync();

        // Act
        await Seed();

        // Assert
        var finalCount = await _context.InstrumentTypes.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}