using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class TransactionSeederTest : BaseSeederTest
{
    private readonly TransactionSeeder _transactionSeeder;
    private readonly PortfolioSeeder _portfolioSeeder;
    private readonly InstrumentSeeder _instrumentSeeder;
    private readonly UserSeeder _userSeeder;
    private readonly StagingSeeder _stagingSeeder;
    private readonly InstrumentTypeSeeder _instrumentTypeSeeder;
    private readonly DateInfoSeeder _dateInfoSeeder;
    private readonly TransactionTypeSeeder _transactionTypeSeeder;

    public TransactionSeederTest(DatabaseFixture fixture) : base(fixture)
    {
        _transactionSeeder = new TransactionSeeder(_context);
        _portfolioSeeder = new PortfolioSeeder(_context, _userManager);
        _instrumentSeeder = new InstrumentSeeder(_context);
        _userSeeder = new UserSeeder(_userManager);
        _stagingSeeder = new StagingSeeder(_context);
        _instrumentTypeSeeder = new InstrumentTypeSeeder(_context);
        _dateInfoSeeder = new DateInfoSeeder(_context);
        _transactionTypeSeeder = new TransactionTypeSeeder(_context);
    }

    private static TransactionDto MapToDto(Transaction transaction)
    {
        var portfolioName = transaction.PortfolioNavigation!.Name!;
        var instrumentName = transaction.InstrumentNavigation!.Name!;
        var bankday = transaction.Bankday!.Value;
        var count = transaction.Count;
        var amount = transaction.Amount;
        var nominal = transaction.Nominal;
        var proportion = transaction.Proportion;

        return new TransactionDto(portfolioName, instrumentName, bankday, count, amount, nominal, proportion);
    }

    private static (string, string, DateOnly, decimal) OrderKey(TransactionDto dto)
    {
        var weight = dto.Count ?? dto.Amount ?? dto.Nominal ?? dto.Proportion ?? 0m;
        return (dto.PortfolioName, dto.InstrumentName, dto.Bankday, weight);
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
    }

    [Fact]
    public async Task Seed_AddsTransactions()
    {
        // Arrange
        var expected = TransactionData
            .GetInitialTransactions()
            .OrderBy(OrderKey)
            .ToList();

        // Act
        await PreSeed();
        await _transactionSeeder.Seed();

        var transactions = await _context.Transactions
            .Include(t => t.PortfolioNavigation)
            .Include(t => t.InstrumentNavigation)
            .ToListAsync();

        var actual = transactions
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
        await _transactionSeeder.Seed();
        var expectedCount = await _context.Transactions.CountAsync();

        // Act
        await _transactionSeeder.Seed();
        var finalCount = await _context.Transactions.CountAsync();

        // Assert
        Assert.Equal(expectedCount, finalCount);
    }
}
