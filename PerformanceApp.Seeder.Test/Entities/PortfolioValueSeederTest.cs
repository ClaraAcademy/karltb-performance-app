using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class PortfolioValueSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    private static PortfolioValueDto MapToDto(PortfolioValue portfolioValue)
    {
        var portfolioName = portfolioValue.PortfolioNavigation!.Name!;
        var bankday = portfolioValue.Bankday;
        var value = portfolioValue.Value!.Value;

        return new PortfolioValueDto(portfolioName, bankday, value);
    }

    private static (string, DateOnly, decimal) OrderKey(PortfolioValueDto dto)
    {
        return (dto.PortfolioName, dto.Bankday, dto.Value);
    }

    [Fact]
    public async Task Seed_InsertsPortfolioValues()
    {
        // Arrange
        var expected = PortfolioValueData
            .PortfolioValues
            .OrderBy(OrderKey)
            .ToList();

        // Act
        var portfolioValues = await _context.PortfolioValues
            .Include(pv => pv.PortfolioNavigation)
            .ToListAsync();

        var actual = portfolioValues
            .Select(MapToDto)
            .OrderBy(OrderKey)
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
        var countBefore = await _context.PortfolioValues.CountAsync();

        // Act
        await _fixture.Seed();

        var countAfter = await _context.PortfolioValues.CountAsync();

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
}