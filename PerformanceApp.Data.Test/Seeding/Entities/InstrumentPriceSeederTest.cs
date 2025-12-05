using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentPriceSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
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
        await Seed();

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
        await Seed();
        var initialCount = await _context.InstrumentPrices.CountAsync();

        // Act
        await Seed();

        // Assert
        var finalCount = await _context.InstrumentPrices.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}