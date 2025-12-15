using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Entities;

public class TransactionTypeSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly TransactionTypeRepository _transactionTypeRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var transactionTypes = await _transactionTypeRepository.GetTransactionTypesAsync();

        return transactionTypes.Any();
    }

    TransactionType MapToTransactionType(string name) => new TransactionType { Name = name };

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var raw = TransactionTypeData.TransactionTypes;

        var transactionTypes = raw.Select(MapToTransactionType).ToList();

        await _transactionTypeRepository.AddTransactionTypesAsync(transactionTypes);
    }
}