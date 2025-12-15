using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class KeyFigureInfoSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    [Fact]
    public async Task Seed_AddsKeyFigureInfos()
    {
        // Arrange
        var expected = KeyFigureData
            .GetKeyFigures()
            .ToList();

        // Act
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
        var expectedCount = await _context.KeyFigureInfos.CountAsync();

        // Act
        await _fixture.Seed();
        var actualCount = await _context.KeyFigureInfos.CountAsync();

        // Assert
        Assert.Equal(expectedCount, actualCount);
    }


}
