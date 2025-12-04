using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection("Seeding collection")]
public class PerformanceTypeSeederTest : BaseSeederTest
{
    private readonly PerformanceTypeSeeder _performanceTypeSeeder;

    public PerformanceTypeSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _performanceTypeSeeder = new PerformanceTypeSeeder(_context);
    }

    [Fact]
    public async Task Seed_AddsPerformanceTypes()
    {
        // Arrange
        var expected = PerformanceTypeData.PerformanceTypes;

        // Act
        await _performanceTypeSeeder.Seed();

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
        await _performanceTypeSeeder.Seed();
        var initialCount = await _context.PerformanceTypeInfos.CountAsync();

        // Act
        await _performanceTypeSeeder.Seed();

        // Assert
        var finalCount = await _context.PerformanceTypeInfos.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}