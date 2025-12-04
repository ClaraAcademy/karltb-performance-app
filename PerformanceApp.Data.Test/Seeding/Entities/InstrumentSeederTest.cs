using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection("Seeding collection")]
public class InstrumentSeederTest : BaseSeederTest
{
    private readonly InstrumentSeeder _instrumentSeeder;
    private readonly StagingSeeder _stagingSeeder;
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder;

    public InstrumentSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _instrumentSeeder = new InstrumentSeeder(_context);
        _stagingSeeder = new StagingSeeder(_context);
        _instrumentTypeSeeder = new InstrumentTypeSeeder(_context);
    }

    [Fact]
    public async Task Seed_AddsInstruments()
    {
        // Arrange
        var expected = InstrumentData.Instruments;

        // Act
        await _stagingSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();

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
        await _stagingSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();
        var initialCount = await _context.Instruments.CountAsync();

        // Act
        await _instrumentSeeder.Seed();

        // Assert
        var finalCount = await _context.Instruments.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}