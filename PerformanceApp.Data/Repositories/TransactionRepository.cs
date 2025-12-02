using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactionsAsync();
    Task AddTransactionAsync(Transaction transaction);
}

public class TransactionRepository(PadbContext context) : ITransactionRepository
{
    private readonly PadbContext _context = context;

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        return await _context.Transactions.ToListAsync();
    }
    public async Task AddTransactionAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
}