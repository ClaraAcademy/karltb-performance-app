using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class StagingSeederTest : BaseSeederTest
{
    private readonly StagingSeeder _stagingSeeder;
    public StagingSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _stagingSeeder = new StagingSeeder(_context);
    }

    private static StagingDto MapToDto(Staging staging)
    {
        return new StagingDto(
            Bankday: staging.Bankday!.Value,
            InstrumentType: staging.InstrumentType!,
            InstrumentName: staging.InstrumentName!,
            Price: staging.Price!.Value
        );
    }

    public static (DateOnly, string, string, decimal) OrderKey(StagingDto dto)
    {
        return (dto.Bankday, dto.InstrumentType, dto.InstrumentName, dto.Price);
    }

    [Fact]
    public async Task Seed_AddsStagingData_WhenDatabaseIsEmpty()
    {
        // Arrange
        var expected = StagingData.Stagings.OrderBy(OrderKey).ToList();
        // Act
        await _stagingSeeder.Seed();

        var stagings = await _context.Stagings.ToListAsync();
        var actual = stagings
            .Select(MapToDto)
            .OrderBy(OrderKey)
            .ToList();

        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(e.InstrumentType, a.InstrumentType);
            Assert.Equal(e.InstrumentName, a.InstrumentName);
            var diff = Math.Abs(e.Price - a.Price);
            Assert.True(diff < 0.0001M);
        }
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        await _stagingSeeder.Seed();
        var initialCount = await _context.Stagings.CountAsync();

        // Act
        await _stagingSeeder.Seed();

        // Assert
        var finalCount = await _context.Stagings.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}