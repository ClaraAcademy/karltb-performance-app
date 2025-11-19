using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Test.Repositories;

public class TransactionRepositoryTest
{
    [Fact]
    public async Task GetTransactionsAsync_ReturnsAllTransactions()
    {
        var context = RepositoryTest.GetContext();
        var repository = new TransactionRepository(context);

        var transactions = new List<Transaction>
        {
            new Transaction { Id = 1, Amount = 100.0m, Created = DateTime.UtcNow },
            new Transaction { Id = 2, Amount = 200.0m, Created = DateTime.UtcNow },
            new Transaction { Id = 3, Amount = 300.0m, Created = DateTime.UtcNow }
        };

        await context.Transactions.AddRangeAsync(transactions);
        await context.SaveChangesAsync();

        var result = await repository.GetTransactionsAsync();

        Assert.Equal(3, result.Count);
        Assert.Contains(result, t => t.Id == 1 && t.Amount == 100.0m);
        Assert.Contains(result, t => t.Id == 2 && t.Amount == 200.0m);
        Assert.Contains(result, t => t.Id == 3 && t.Amount == 300.0m);
    }

    [Fact]
    public async Task GetTransactionsAsync_ReturnsEmptyList_WhenNoTransactionsExist()
    {
        var context = RepositoryTest.GetContext();
        var repository = new TransactionRepository(context);

        var result = await repository.GetTransactionsAsync();

        Assert.Empty(result);
    }
}