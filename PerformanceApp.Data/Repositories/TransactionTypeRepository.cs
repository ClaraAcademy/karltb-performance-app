using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface ITransactionTypeRepository
{
    void AddTransactionType(TransactionType transactionType);
    Task AddTransactionTypeAsync(TransactionType transactionType);
    void AddTransactionTypes(List<TransactionType> transactionTypes);
    Task AddTransactionTypesAsync(List<TransactionType> transactionTypes);
}

public class TransactionTypeRepository(PadbContext context) : ITransactionTypeRepository
{
    private readonly PadbContext _context = context;

    public void AddTransactionType(TransactionType transactionType)
    {
        _context.TransactionTypes.Add(transactionType);
        _context.SaveChanges();
    }

    public void AddTransactionTypes(List<TransactionType> transactionTypes)
    {
        _context.TransactionTypes.AddRange(transactionTypes);
        _context.SaveChanges();
    }
    public async Task AddTransactionTypeAsync(TransactionType transactionType)
    {
        await _context.TransactionTypes.AddAsync(transactionType);
        await _context.SaveChangesAsync();
    }

    public async Task AddTransactionTypesAsync(List<TransactionType> transactionTypes)
    {
        await _context.TransactionTypes.AddRangeAsync(transactionTypes);
        await _context.SaveChangesAsync();
    }
}