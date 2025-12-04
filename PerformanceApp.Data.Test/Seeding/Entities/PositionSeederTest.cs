using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class PositionSeederTest : BaseSeederTest
{
    private readonly PositionSeeder _positionSeeder;
    private readonly StagingSeeder _stagingSeeder;
    private readonly DateInfoSeeder _dateInfoSeeder;
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder;
    private readonly InstrumentSeeder _instrumentSeeder;
    private readonly TransactionTypeSeeder _transactionTypeSeeder;
    private readonly UserSeeder _userSeeder;
    private readonly PortfolioSeeder _portfolioSeeder;
    private readonly TransactionSeeder _transactionSeeder;

    public PositionSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _positionSeeder = new PositionSeeder(_context);
        _stagingSeeder = new StagingSeeder(_context);
        _dateInfoSeeder = new DateInfoSeeder(_context);
        _instrumentTypeSeeder = new InstrumentTypeSeeder(_context);
        _instrumentSeeder = new InstrumentSeeder(_context);
        _transactionTypeSeeder = new TransactionTypeSeeder(_context);
        _userSeeder = new UserSeeder(_userManager);
        _portfolioSeeder = new PortfolioSeeder(_context, _userManager);
        _transactionSeeder = new TransactionSeeder(_context);
    }

    private async Task PreSeed()
    {
        await _stagingSeeder.Seed();
        await _dateInfoSeeder.Seed();
        await _instrumentTypeSeeder.Seed();
        await _instrumentSeeder.Seed();
        await _transactionTypeSeeder.Seed();
        await _userSeeder.Seed();
        await _portfolioSeeder.Seed();
        await _transactionSeeder.Seed();
    }

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
        await PreSeed();
        await _positionSeeder.Seed();

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
        await PreSeed();

        // Act
        await _positionSeeder.Seed();
        var firstRunCount = await _context.Positions.CountAsync();

        await _positionSeeder.Seed();
        var secondRunCount = await _context.Positions.CountAsync();

        // Assert
        Assert.Equal(firstRunCount, secondRunCount);
    }
}