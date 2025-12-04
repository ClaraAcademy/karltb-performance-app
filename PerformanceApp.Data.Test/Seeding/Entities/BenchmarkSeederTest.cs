using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection("Seeding collection")]
public class BenchmarkSeederTest : BaseSeederTest
{
    private readonly BenchmarkSeeder _benchmarkSeeder;
    private readonly PortfolioSeeder _portfolioSeeder;
    private readonly UserSeeder _userSeeder;

    public BenchmarkSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _benchmarkSeeder = new BenchmarkSeeder(_context);
        _portfolioSeeder = new PortfolioSeeder(_context, _userManager);
        _userSeeder = new UserSeeder(_userManager);
    }

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
        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();
        await _benchmarkSeeder.Seed();

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
        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();
        await _benchmarkSeeder.Seed();
        var initialCount = await _context.Benchmarks.CountAsync();

        // Act
        await _benchmarkSeeder.Seed();

        // Assert
        var finalCount = await _context.Benchmarks.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

}