using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    [Fact]
    public async Task Seed_AddsInstruments()
    {
        // Arrange
        var expected = InstrumentData.Instruments;

        // Act

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
        var initialCount = await _context.Instruments.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.Instruments.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}