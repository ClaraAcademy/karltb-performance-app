using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class BenchmarkSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private static PortfolioBenchmarkDto MapToDto(Benchmark benchmark)
    {
        var portfolioName = benchmark.PortfolioPortfolioNavigation!.Name!;
        var benchmarkName = benchmark.BenchmarkPortfolioNavigation!.Name!;
        return new (portfolioName, benchmarkName);
    }

    private static (string, string) OrderKey(PortfolioBenchmarkDto dto)
    {
        return (dto.PortfolioName, dto.BenchmarkName);
    }

    [Fact]
    public async Task Seed_AddsBenchmarks()
    {
        // Arrange
        var expected = new List<PortfolioBenchmarkDto>
        {
            new(PortfolioData.PortfolioA, PortfolioData.BenchmarkA),
            new(PortfolioData.PortfolioB, PortfolioData.BenchmarkB),
        }
        .OrderBy(OrderKey)
        .ToList();

        // Act
        await Seed();

        var benchmarks = await _context.Benchmarks
            .Include(b => b.PortfolioPortfolioNavigation)
            .Include(b => b.BenchmarkPortfolioNavigation)
            .ToListAsync();

        var actual = benchmarks
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
        await Seed();
        var initialCount = await _context.Benchmarks.CountAsync();

        // Act
        await Seed();

        // Assert
        var finalCount = await _context.Benchmarks.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}