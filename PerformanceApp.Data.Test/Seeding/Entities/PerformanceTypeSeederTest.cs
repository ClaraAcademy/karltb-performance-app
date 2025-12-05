using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class PerformanceTypeSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    [Fact]
    public async Task Seed_AddsPerformanceTypes()
    {
        // Arrange
        var expected = PerformanceTypeData.PerformanceTypes;

        // Act
        await Seed();

        var performanceTypes = await _context.PerformanceTypeInfos.ToListAsync();
        var actual = performanceTypes
            .Select(pt => pt.Name)
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
        var initialCount = await _context.PerformanceTypeInfos.CountAsync();

        // Act
        await Seed();

        // Assert
        var finalCount = await _context.PerformanceTypeInfos.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}