using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Seeding.Queries;

namespace PerformanceApp.Data.Seeding.Entities;

public class TransactionSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;

    public async Task Seed()
    {
        var queries = TransactionQueries.GetBuyQueries();

        foreach (var q in queries)
        {
            await SqlExecutor.ExecuteQueryAsync(_context, q);
        }
    }
}