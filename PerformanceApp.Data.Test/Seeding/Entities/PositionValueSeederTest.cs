using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class PositionValueSeederTest : BaseSeederTest
{
    private readonly PositionValueSeeder _positionValueSeeder;
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

    public PositionValueSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _positionValueSeeder = new PositionValueSeeder(_context);
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
    }

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
        await PreSeed();
        await _positionValueSeeder.Seed();

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
        await PreSeed();
        await _positionValueSeeder.Seed();

        var countBefore = await _context.PositionValues.CountAsync();

        // Act
        await _positionValueSeeder.Seed();

        // Assert
        var countAfter = await _context.PositionValues.CountAsync();
        Assert.Equal(countBefore, countAfter);
    }



}