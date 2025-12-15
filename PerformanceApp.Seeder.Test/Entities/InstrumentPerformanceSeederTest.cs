using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentPerformanceSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;

    private static InstrumentPerformanceDto MapToDto(InstrumentPerformance ip)
    {
        return new InstrumentPerformanceDto(
            ip.InstrumentNavigation.Name!,
            ip.PerformanceTypeNavigation.Name!,
            ip.PeriodStart,
            ip.PeriodEnd,
            ip.Value
        );
    }

    private static (string, string, DateOnly, DateOnly, decimal) OrderKey(InstrumentPerformanceDto dto)
    {
        return (dto.InstrumentName, dto.PerformanceType, dto.PeriodStart, dto.PeriodEnd, dto.Value);
    }

    [Fact]
    public async Task Seed_AddsInstrumentPerformances()
    {
        // Arrange
        var expected = InstrumentPerformanceData
            .InstrumentPerformances
            .OrderBy(OrderKey)
            .ToList();

        // Act

        var instrumentPerformances = await _context
            .InstrumentPerformances
            .Include(ip => ip.InstrumentNavigation)
            .Include(ip => ip.PerformanceTypeNavigation)
            .ToListAsync();

        var actual = instrumentPerformances
            .Select(MapToDto)
            .OrderBy(OrderKey)
            .ToList();

        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);

        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e,a) in expected.Zip(actual))
        {
            Assert.Equal(e.InstrumentName, a.InstrumentName);
            Assert.Equal(e.PerformanceType, a.PerformanceType);
            Assert.Equal(e.PeriodStart, a.PeriodStart);
            Assert.Equal(e.PeriodEnd, a.PeriodEnd);
            var diff = Math.Abs(e.Value - a.Value);
            Assert.True(diff < 0.0000001m, $"Value difference {diff} is too large.");
        }
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        var expectedCount = await _context.InstrumentPerformances.CountAsync();

        // Act
        await _fixture.Seed();
        var actualCount = await _context.InstrumentPerformances.CountAsync();

        // Assert
        Assert.Equal(expectedCount, actualCount);
    }

}