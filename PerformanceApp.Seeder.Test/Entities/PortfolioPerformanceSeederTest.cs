using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class PortfolioPerformanceSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    private static PortfolioPerformanceDto MapToDto(PortfolioPerformance pp)
    {
        return new PortfolioPerformanceDto(
            pp.PortfolioNavigation.Name,
            pp.PerformanceTypeNavigation.Name,
            pp.PeriodStart,
            pp.PeriodEnd,
            pp.Value
        );
    }

    private static (string, string, DateOnly, DateOnly, decimal) OrderKey(PortfolioPerformanceDto dto)
    {
        return (dto.PortfolioName, dto.PerformanceType, dto.PeriodStart, dto.PeriodEnd, dto.Value);
    }

    [Fact]
    public async Task Seed_AddsPortfolioPerformances()
    {
        // Arrange
        var expected = PortfolioPerformanceData
            .PortfolioPerformances
            .OrderBy(OrderKey)
            .ToList();

        // Act

        var portfolioPerformances = await _context
            .PortfolioPerformances
            .Include(pp => pp.PortfolioNavigation)
            .Include(pp => pp.PerformanceTypeNavigation)
            .ToListAsync();

        var actual = portfolioPerformances
            .Select(MapToDto)
            .OrderBy(OrderKey)
            .ToList();


        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);

        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e,a) in expected.Zip(actual))
        {
            Assert.Equal(e.PortfolioName, a.PortfolioName);
            Assert.Equal(e.PerformanceType, a.PerformanceType);
            Assert.Equal(e.PeriodStart, a.PeriodStart);
            Assert.Equal(e.PeriodEnd, a.PeriodEnd);
            var diff = Math.Abs(e.Value - a.Value);
            Assert.True(diff < 0.0001m, $"Expected {e.Value} but got {a.Value}");
        }
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        var expectedCount = await _context.PortfolioPerformances.CountAsync();

        // Act
        await _fixture.Seed();
        var actualCount = await _context.PortfolioPerformances.CountAsync();

        // Assert
        Assert.Equal(expectedCount, actualCount);
    }
}