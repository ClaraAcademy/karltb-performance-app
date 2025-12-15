using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentPriceSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    private static InstrumentPriceDto MapToDto(InstrumentPrice ip)
    {
        var bankday = ip.Bankday;
        var instrumentName = ip.InstrumentNavigation.Name!;
        var instrumentType = ip.InstrumentNavigation.InstrumentTypeNavigation!.Name;
        var price = ip.Price;
        return new InstrumentPriceDto(bankday, instrumentName, instrumentType, price);
    }
    private static InstrumentPriceDto MapToDto(StagingDto staging)
    {
        var bankday = staging.Bankday;
        var instrumentName = staging.InstrumentName;
        var instrumentType = staging.InstrumentType;
        var price = staging.Price;
        return new InstrumentPriceDto(bankday, instrumentName, instrumentType, price);
    }

    public static (DateOnly, string, string, decimal) OrderKey(InstrumentPriceDto dto)
    {
        return (dto.Bankday, dto.InstrumentType, dto.InstrumentName, dto.Price);
    }

    [Fact]
    public async Task Seed_AddsInstrumentPrices()
    {
        // Arrange
        var expected = StagingData.Stagings
            .Select(MapToDto)
            .OrderBy(OrderKey)
            .ToList();

        // Act

        var instrumentPrices = await _context.InstrumentPrices
            .Include(ip => ip.InstrumentNavigation)
                .ThenInclude(i => i.InstrumentTypeNavigation)
            .ToListAsync();
        
        var actual = instrumentPrices
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
        var initialCount = await _context.InstrumentPrices.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.InstrumentPrices.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}