using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Builders;

namespace PerformanceApp.Infrastructure.Test.Repositories;

public class TransactionTypeRepositoryTest : BaseRepositoryTest
{
    private readonly TransactionTypeRepository _repository;

    public TransactionTypeRepositoryTest()
    {
        _repository = new TransactionTypeRepository(_context);
    }

    [Fact]
    public async Task AddTransactionTypesAsync_AddsTransactionTypesToDatabase()
    {
        // Arrange
        var expected = new TransactionTypeBuilder()
            .Many(5)
            .ToList();

        await _repository.AddTransactionTypesAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var actual = await _context.TransactionTypes.ToListAsync();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task AddTransactionTypesAsync_EmptyList_DoesNotAddAnything()
    {
        // Arrange
        var empty = new List<TransactionType>();

        // Act
        await _repository.AddTransactionTypesAsync(empty);
        await _context.SaveChangesAsync();

        // Assert
        var actual = await _context.TransactionTypes.ToListAsync();
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetTransactionTypesAsync_ReturnsAllTransactionTypes()
    {
        // Arrange
        var expected = new TransactionTypeBuilder()
            .Many(10)
            .ToList();

        await _context.TransactionTypes.AddRangeAsync(expected);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetTransactionTypesAsync();
        var actual = result.ToList();

        // Assert
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.Id, a.Id);
            Assert.Equal(e.Name, a.Name);
        }
    }

    [Fact]
    public async Task GetTransactionTypesAsync_NoTransactionTypes_ReturnsEmptyList()
    {
        // Act
        var actual = await _repository.GetTransactionTypesAsync();

        // Assert
        Assert.Empty(actual);
    }
}