using Microsoft.EntityFrameworkCore;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface ITransactionTypeRepository
{
    Task AddTransactionTypesAsync(List<TransactionType> transactionTypes);
    Task<IEnumerable<TransactionType>> GetTransactionTypesAsync();
}

public class TransactionTypeRepository(PadbContext context) : ITransactionTypeRepository
{
    private readonly PadbContext _context = context;

    public async Task AddTransactionTypesAsync(List<TransactionType> transactionTypes)
    {
        await _context.TransactionTypes.AddRangeAsync(transactionTypes);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<TransactionType>> GetTransactionTypesAsync()
    {
        return await _context.TransactionTypes.ToListAsync();
    }
}