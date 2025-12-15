using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Infrastructure.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task AddTransactionsAsync(IEnumerable<Transaction> transactions);
}

public class TransactionRepository(PadbContext context) : ITransactionRepository
{
    private readonly PadbContext _context = context;

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        return await _context.Transactions.ToListAsync();
    }
    public async Task AddTransactionsAsync(IEnumerable<Transaction> transactions)
    {
        await _context.Transactions.AddRangeAsync(transactions);
        await _context.SaveChangesAsync();
    }
}