using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentTypeSeederTest : BaseSeederTest
{
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder;
    private readonly StagingSeeder _stagingSeeder;

    public InstrumentTypeSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _instrumentTypeSeeder = new InstrumentTypeSeeder(_context);
        _stagingSeeder = new StagingSeeder(_context);
    }

    [Fact]
    public async Task Seed_AddsInstrumentTypes()
    {
        // Arrange
        var expected = InstrumentTypeData.InstrumentTypes;

        // Act
        await _stagingSeeder.Seed();
        await _instrumentTypeSeeder.Seed();

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
        await _instrumentTypeSeeder.Seed();
        var initialCount = await _context.InstrumentTypes.CountAsync();

        // Act
        await _instrumentTypeSeeder.Seed();

        // Assert
        var finalCount = await _context.InstrumentTypes.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}