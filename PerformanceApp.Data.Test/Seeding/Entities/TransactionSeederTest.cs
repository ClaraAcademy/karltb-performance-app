using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class TransactionSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
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

    [Fact]
    public async Task Seed_AddsTransactions()
    {
        // Arrange
        var expected = TransactionData
            .GetInitialTransactions()
            .OrderBy(OrderKey)
            .ToList();

        // Act
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
        var expectedCount = await _context.Transactions.CountAsync();

        // Act
        await _fixture.Seed();
        var finalCount = await _context.Transactions.CountAsync();

        // Assert
        Assert.Equal(expectedCount, finalCount);
    }
}
