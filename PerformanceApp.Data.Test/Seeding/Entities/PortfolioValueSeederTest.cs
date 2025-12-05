using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class PortfolioValueSeederTest : BaseSeederTest
{
    private readonly PortfolioValueSeeder _portfolioValueSeeder;
    private readonly StagingSeeder _stagingSeeder;
    private readonly DateInfoSeeder _dateInfoSeeder;
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder;
    private readonly InstrumentSeeder _instrumentSeeder;
    private readonly InstrumentPriceSeeder _instrumentPriceSeeder;
    private readonly TransactionTypeSeeder _transactionTypeSeeder;
    private readonly UserSeeder _userSeeder;
    private readonly PortfolioSeeder _portfolioSeeder;
    private readonly BenchmarkSeeder _benchmarkSeeder;
    private readonly TransactionSeeder _transactionSeeder;
    private readonly PositionSeeder _positionSeeder;
    private readonly PositionValueSeeder _positionValueSeeder;

    public PortfolioValueSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _portfolioValueSeeder = new PortfolioValueSeeder(_context);
        _stagingSeeder = new StagingSeeder(_context);
        _dateInfoSeeder = new DateInfoSeeder(_context);
        _instrumentTypeSeeder = new InstrumentTypeSeeder(_context);
        _instrumentSeeder = new InstrumentSeeder(_context);
        _instrumentPriceSeeder = new InstrumentPriceSeeder(_context);
        _transactionTypeSeeder = new TransactionTypeSeeder(_context);
        _userSeeder = new UserSeeder(_userManager);
        _portfolioSeeder = new PortfolioSeeder(_context, _userManager);
        _benchmarkSeeder = new BenchmarkSeeder(_context);
        _transactionSeeder = new TransactionSeeder(_context);
        _positionSeeder = new PositionSeeder(_context);
        _positionValueSeeder = new PositionValueSeeder(_context);
    }

    private async Task PreSeed()
    {
        await _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();
        await _instrumentPriceSeeder.Seed();
        await _transactionTypeSeeder.Seed();
        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();
        await _benchmarkSeeder.Seed();
        await _transactionSeeder.Seed();
        await _positionSeeder.Seed();
        await _positionValueSeeder.Seed();

    }

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
        await PreSeed();
        await _portfolioValueSeeder.Seed();

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
        await PreSeed();
        await _portfolioValueSeeder.Seed();

        var countBefore = await _context.PortfolioValues.CountAsync();

        // Act
        await _portfolioValueSeeder.Seed();

        var countAfter = await _context.PortfolioValues.CountAsync();

        // Assert
        Assert.Equal(countBefore, countAfter);
    }
}