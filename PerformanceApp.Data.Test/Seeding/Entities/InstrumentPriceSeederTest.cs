using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class InstrumentPriceSeederTest : BaseSeederTest
{
    private readonly InstrumentPriceSeeder _instrumentPriceSeeder;
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder;
    private readonly InstrumentSeeder _instrumentSeeder;
    private readonly DateInfoSeeder _dateInfoSeeder;
    private readonly StagingSeeder _stagingSeeder;

    public InstrumentPriceSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _instrumentPriceSeeder = new InstrumentPriceSeeder(_context);
        _instrumentTypeSeeder = new InstrumentTypeSeeder(_context);
        _instrumentSeeder = new InstrumentSeeder(_context);
        _dateInfoSeeder = new DateInfoSeeder(_context);
        _stagingSeeder = new StagingSeeder(_context);
    }

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

        await _stagingSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _dateInfoSeeder.Seed();
        await _instrumentSeeder.Seed();

        // Act
        await _instrumentPriceSeeder.Seed();

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
        await _stagingSeeder.Seed();
        await _instrumentPriceSeeder.Seed();
        var initialCount = await _context.InstrumentPrices.CountAsync();

        // Act
        await _instrumentPriceSeeder.Seed();

        // Assert
        var finalCount = await _context.InstrumentPrices.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}