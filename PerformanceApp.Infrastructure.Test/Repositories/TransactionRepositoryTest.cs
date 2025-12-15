using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class TransactionRepositoryTest : BaseRepositoryTest
{
    private readonly TransactionRepository _repository;

    public TransactionRepositoryTest()
    {
        _repository = new TransactionRepository(_context);
    }

    private static Transaction CreateTransaction(int i)
    {
        return new Transaction
        {
            Amount = 100.0m * i,
            Created = DateTime.Now
        };
    }

    private static List<Transaction> CreateTransactions(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => CreateTransaction(i))
            .ToList();
    }

    [Fact]
    public async Task GetTransactionsAsync_ReturnsAllTransactions()
    {
        // Arrange
        var nExpected = 7;
        var transactions = CreateTransactions(nExpected);

        await _context.Transactions.AddRangeAsync(transactions);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetTransactionsAsync();

        // Assert
        var nActual = result.Count;
        Assert.Equal(nExpected, nActual);
        foreach (var (e, a) in transactions.Zip(result))
        {
            Assert.Equal(e.Amount, a.Amount);
        }
    }

    [Fact]
    public async Task GetTransactionsAsync_ReturnsEmptyList_WhenNoTransactionsExist()
    {
        // Act
        var result = await _repository.GetTransactionsAsync();

        // Assert
        Assert.Empty(result);
    }
}