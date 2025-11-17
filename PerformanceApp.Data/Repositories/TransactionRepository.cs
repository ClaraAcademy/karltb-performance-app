using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactionsAsync();
}

public class TransactionRepository(PadbContext context) : ITransactionRepository
{
    private readonly PadbContext _context = context;

    public async Task<List<Transaction>> GetTransactionsAsync()
    {
        return await _context.Transactions.ToListAsync();
    }
}