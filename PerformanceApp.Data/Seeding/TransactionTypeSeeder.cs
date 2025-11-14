using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class TransactionTypeSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly TransactionTypeRepository _transactionTypeRepository = new(context);


    TransactionType MapToTransactionType(string name) => new TransactionType { TransactionTypeName = name };

    public void Seed()
    {
        var raw = new List<string> { "Buy", "Sell" };

        var transactionTypes = raw.Select(MapToTransactionType)
            .ToList();

        _transactionTypeRepository.AddTransactionTypes(transactionTypes);

        _context.SaveChanges();
    }
}