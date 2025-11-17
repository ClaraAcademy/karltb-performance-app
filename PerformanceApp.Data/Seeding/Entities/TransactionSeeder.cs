using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Seeding.Queries;

namespace PerformanceApp.Data.Seeding.Entities;

public class TransactionSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly ITransactionRepository _transactionRepository = new TransactionRepository(context);

    private async Task<bool> IsPopulated()
    {
        var transactions = await _transactionRepository.GetTransactionsAsync();
        return transactions.Any();
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var queries = TransactionQueries.GetBuyQueries();

        foreach (var q in queries)
        {
            await SqlExecutor.ExecuteQueryAsync(_context, q);
        }
    }
}