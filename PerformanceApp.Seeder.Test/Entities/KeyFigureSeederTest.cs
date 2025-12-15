using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class KeyFigureSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    private static KeyFigureValueDto MapToDto(KeyFigureValue kfv)
    {
        return new KeyFigureValueDto(
            kfv.PortfolioNavigation.Name,
            kfv.KeyFigureInfoNavigation.Name,
            kfv.Value ?? throw new InvalidOperationException("KeyFigureValue.Value is null")
        );
    }

    private static (string, string, decimal) OrderKey(KeyFigureValueDto dto)
    {
        return (dto.PortfolioName, dto.KeyFigureName, dto.Value);
    }

    [Fact]
    public async Task Seed_AddsKeyFigures()
    {
        // Arrange
        var expected = KeyFigureValueData
            .KeyFigureValues
            .OrderBy(OrderKey)
            .ToList();

        // Act
        var keyFigures = await _context
            .KeyFigureValues
            .Include(kfv => kfv.PortfolioNavigation)
            .Include(kfv => kfv.KeyFigureInfoNavigation)
            .ToListAsync();

        var actual = keyFigures
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
            Assert.Equal(e.KeyFigureName, a.KeyFigureName);
            var diff = Math.Abs(e.Value - a.Value);
            var tolerance = 0.0000001M;
            Assert.True(diff <= tolerance, $"Expected {e.Value} but got {a.Value} which differs by more than {tolerance}");
        }
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        var expected = await _context.KeyFigureValues.CountAsync();

        // Act
        await _fixture.Seed();
        var actual = await _context.KeyFigureValues.CountAsync();

        // Assert
        Assert.Equal(expected, actual);
    }

}