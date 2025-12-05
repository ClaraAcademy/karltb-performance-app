using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class KeyFigureInfoSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    [Fact]
    public async Task Seed_AddsKeyFigureInfos()
    {
        // Arrange
        var expected = KeyFigureData
            .GetKeyFigures()
            .ToList();

        // Act
        await Seed();

        var keyFigureInfos = await _context.KeyFigureInfos.ToListAsync();
        var actual = keyFigureInfos
            .Select(kf => kf.Name)
            .ToList();
        
        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        await Seed();
        var expectedCount = await _context.KeyFigureInfos.CountAsync();

        // Act
        await Seed();
        var actualCount = await _context.KeyFigureInfos.CountAsync();

        // Assert
        Assert.Equal(expectedCount, actualCount);
    }


}
