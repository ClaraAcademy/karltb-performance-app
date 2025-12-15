using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class PositionSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    private static PositionDto MapToDto(Position position)
    {
        var portfolioName = position.PortfolioNavigation!.Name!;
        var instrumentName = position.InstrumentNavigation!.Name!;
        var bankday = position.Bankday!.Value;
        var count = position.Count;
        var amount = position.Amount;
        var proportion = position.Proportion;
        var nominal = position.Nominal;

        return new PositionDto(portfolioName, instrumentName, bankday, count, amount, proportion, nominal);
    }

    private static (string, string, DateOnly, decimal) OrderKey(PositionDto dto)
    {
        var weight = dto.Count ?? dto.Amount ?? dto.Nominal ?? dto.Proportion ?? 0m;
        return (dto.PortfolioName, dto.InstrumentName, dto.Bankday, weight);
    }

    [Fact]
    public async Task Seed_InsertsPositions()
    {
        // Arrange
        var expected = PositionData
            .Positions
            .OrderBy(OrderKey)
            .ToList();
        
        // Act
        var positions = await _context.Positions
            .Include(p => p.PortfolioNavigation)
            .Include(p => p.InstrumentNavigation)
            .ToListAsync();

        var actual = positions
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
        var firstRunCount = await _context.Positions.CountAsync();

        // Act
        await _fixture.Seed();
        var secondRunCount = await _context.Positions.CountAsync();

        // Assert
        Assert.Equal(firstRunCount, secondRunCount);
    }
}