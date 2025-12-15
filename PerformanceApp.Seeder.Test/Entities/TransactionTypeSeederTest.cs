using Microsoft.EntityFrameworkCore;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Test.Entities;

[Collection(SeedingCollection.Name)]
public class TransactionTypeSeederTest(DatabaseFixture fixture) : BaseSeederTest(fixture)
{
    private readonly DatabaseFixture _fixture = fixture;
    [Fact]
    public async Task Seed_AddsTransactionTypes()
    {
        // Arrange
        var expected = TransactionTypeData.TransactionTypes;

        // Act
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
        var initialCount = await _context.TransactionTypes.CountAsync();

        // Act
        await _fixture.Seed();

        // Assert
        var finalCount = await _context.TransactionTypes.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }


}