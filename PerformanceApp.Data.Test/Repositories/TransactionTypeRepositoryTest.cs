using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class TransactionTypeRepositoryTest
{
    [Fact]
    public async Task AddTransactionTypesAsync_AddsTransactionTypesToDatabase()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new TransactionTypeRepository(context);

        var transactionTypes = new List<TransactionType>
        {
            new TransactionType { Id = 1, Name = "Type1" },
            new TransactionType { Id = 2, Name = "Type2" }
        };

        // Act
        await repository.AddTransactionTypesAsync(transactionTypes);
        await context.SaveChangesAsync();
        // Assert
        var addedTransactionTypes = await context.TransactionTypes.ToListAsync();
        Assert.Equal(2, addedTransactionTypes.Count);
        Assert.Contains(addedTransactionTypes, tt => tt.Name == "Type1");
        Assert.Contains(addedTransactionTypes, tt => tt.Name == "Type2");
    }

    [Fact]
    public async Task AddTransactionTypesAsync_EmptyList_DoesNotAddAnything()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new TransactionTypeRepository(context);

        var transactionTypes = new List<TransactionType>();

        // Act
        await repository.AddTransactionTypesAsync(transactionTypes);
        await context.SaveChangesAsync();

        // Assert
        var addedTransactionTypes = await context.TransactionTypes.ToListAsync();
        Assert.Empty(addedTransactionTypes);
    }

    [Fact]
    public async Task GetTransactionTypesAsync_ReturnsAllTransactionTypes()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new TransactionTypeRepository(context);

        var transactionTypes = new List<TransactionType>
        {
            new TransactionType { Id = 1, Name = "Type1" },
            new TransactionType { Id = 2, Name = "Type2" }
        };

        await context.TransactionTypes.AddRangeAsync(transactionTypes);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetTransactionTypesAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, tt => tt.Name == "Type1");
        Assert.Contains(result, tt => tt.Name == "Type2");
    }

    [Fact]
    public async Task GetTransactionTypesAsync_NoTransactionTypes_ReturnsEmptyList()
    {
        // Arrange
        var context = BaseRepositoryTest.GetContext();
        var repository = new TransactionTypeRepository(context);

        // Act
        var result = await repository.GetTransactionTypesAsync();

        // Assert
        Assert.Empty(result);
    }
}