using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Entities;

namespace PerformanceApp.Data.Test.Seeding.Entities;

[Collection(SeedingCollection.Name)]
public class TransactionTypeSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    [Fact]
    public async Task Seed_AddsTransactionTypes()
    {
        // Arrange
        var expected = TransactionTypeData.TransactionTypes;

        // Act
        await Seed();

        var transactionTypes = await _context.TransactionTypes.ToListAsync();
        var actual = transactionTypes
            .Select(tt => tt.Name)
            .OrderBy(n => n)
            .ToList();

        // Assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        Assert.Equal(expected, actual!);
    }

    [Fact]
    public async Task Seed_IsIdempotent()
    {
        // Arrange
        await Seed();
        var initialCount = await _context.TransactionTypes.CountAsync();

        // Act
        await Seed();

        // Assert
        var finalCount = await _context.TransactionTypes.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }


}