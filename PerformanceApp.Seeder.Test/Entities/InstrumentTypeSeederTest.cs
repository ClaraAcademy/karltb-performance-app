using Microsoft.EntityFrameworkCore;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentTypeSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    [Fact]
    public async Task Seed_AddsInstrumentTypes()
    {
        // Arrange
        var expected = InstrumentTypeData.InstrumentTypes;

        // Act

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
        var initialCount = await _context.InstrumentTypes.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.InstrumentTypes.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}