using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class StagingSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
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
        var initialCount = await _context.Stagings.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.Stagings.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }
}