using System.Threading.Tasks;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Entities;

public class TransactionTypeSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly TransactionTypeRepository _transactionTypeRepository = new(context);


    TransactionType MapToTransactionType(string name) => new TransactionType { TransactionTypeName = name };

    public async Task Seed()
    {
        var raw = new List<string> { "Buy", "Sell" };

        var transactionTypes = raw.Select(MapToTransactionType).ToList();

        await _transactionTypeRepository.AddTransactionTypesAsync(transactionTypes);
    }
}