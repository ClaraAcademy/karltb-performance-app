using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class PositionValueSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    private static PositionValueDto MapToDto(PositionValue positionValue)
    {
        var portfolioName = positionValue
            .PositionNavigation!
            .PortfolioNavigation!
            .Name!;
        var instrumentName = positionValue
            .PositionNavigation!
            .InstrumentNavigation!
            .Name!;
        var bankday = positionValue.Bankday;
        var value = positionValue
            .Value!
            .Value;

        return new PositionValueDto(portfolioName, instrumentName, bankday, value);
    }

    private static (string, string, DateOnly, decimal) OrderKey(PositionValueDto dto)
    {
        return (dto.PortfolioName, dto.InstrumentName, dto.Bankday, dto.Value);
    }

    [Fact]
    public async Task Seed_InsertsPositionValues()
    {
        // Arrange
        var expected = PositionValueData
            .PositionValues
            .OrderBy(OrderKey)
            .ToList();

        // Act
        var positionValues = await _context.PositionValues
            .Include(pv => pv.PositionNavigation)
                .ThenInclude(p => p.PortfolioNavigation)
            .Include(pv => pv.PositionNavigation)
                .ThenInclude(p => p.InstrumentNavigation)
            .ToListAsync();

        var actual = positionValues
            .Select(MapToDto)
            .OrderBy(OrderKey)
            .ToList();

        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.PortfolioName, a.PortfolioName);
            Assert.Equal(e.InstrumentName, a.InstrumentName);
            Assert.Equal(e.Bankday, a.Bankday);
            var diff = Math.Abs(e.Value - a.Value);
            Assert.True(diff < 0.001M, $"Expected {e}, Actual: {a}");
        }
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        var countBefore = await _context.PositionValues.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var countAfter = await _context.PositionValues.CountAsync();
        Assert.Equal(countBefore, countAfter);
    }



}