using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Test.Repositories;

public class TransactionTypeRepositoryTest : BaseRepositoryTest
{
    private readonly TransactionTypeRepository _repository;

    public TransactionTypeRepositoryTest()
    {
        _repository = new TransactionTypeRepository(_context);
    }

    private static TransactionType CreateTransactionType(int i)
    {
        return new TransactionType { Id = i, Name = $"Type{i}" };
    }

    private static List<TransactionType> CreateTransactionTypes(int count)
    {
        return Enumerable.Range(1, count)
            .Select(i => CreateTransactionType(i))
            .ToList();
    }

    private static void AreEqual(TransactionType expected, TransactionType actual)
    {
        Assert.Equal(expected.Id, actual.Id);
        Assert.Equal(expected.Name, actual.Name);
    }
    private static void AssertEqual(IEnumerable<TransactionType> expected, IEnumerable<TransactionType> actual)
    {
        Assert.Equal(expected.Count(), actual.Count());
        foreach (var (e, a) in expected.Zip(actual))
        {
            AreEqual(e, a);
        }
    }

    [Fact]
    public async Task AddTransactionTypesAsync_AddsTransactionTypesToDatabase()
    {
        // Arrange
        var nExpected = 9;
        var transactionTypes = CreateTransactionTypes(nExpected);
        await _repository.AddTransactionTypesAsync(transactionTypes);
        await _context.SaveChangesAsync();

        // Act
        var addedTransactionTypes = await _context.TransactionTypes.ToListAsync();

        // Assert
        AssertEqual(transactionTypes, addedTransactionTypes);
    }

    [Fact]
    public async Task AddTransactionTypesAsync_EmptyList_DoesNotAddAnything()
    {
        // Arrange
        var transactionTypes = new List<TransactionType>();

        // Act
        await _repository.AddTransactionTypesAsync(transactionTypes);
        await _context.SaveChangesAsync();

        // Assert
        var addedTransactionTypes = await _context.TransactionTypes.ToListAsync();
        Assert.Empty(addedTransactionTypes);
    }

    [Fact]
    public async Task GetTransactionTypesAsync_ReturnsAllTransactionTypes()
    {
        // Arrange
        var nExpected = 100;
        var transactionTypes = CreateTransactionTypes(nExpected);

        await _context.TransactionTypes.AddRangeAsync(transactionTypes);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetTransactionTypesAsync();

        // Assert
        AssertEqual(transactionTypes, result);
    }

    [Fact]
    public async Task GetTransactionTypesAsync_NoTransactionTypes_ReturnsEmptyList()
    {
        // Act
        var result = await _repository.GetTransactionTypesAsync();

        // Assert
        Assert.Empty(result);
    }
}