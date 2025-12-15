using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Entities;

public class TransactionSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly ITransactionRepository _transactionRepository = new TransactionRepository(context);

    private async Task<bool> IsPopulated()
    {
        var transactions = await _transactionRepository.GetTransactionsAsync();
        return transactions.Any();
    }

    private async Task<Transaction> Map(TransactionDto dto)
    {
        var portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.Name == dto.PortfolioName);
        var instrument = await _context.Instruments.FirstOrDefaultAsync(i => i.Name == dto.InstrumentName);

        return new Transaction
        {
            PortfolioNavigation = portfolio,
            InstrumentNavigation = instrument,
            Bankday = dto.Bankday,
            Count = dto.Count,
            Nominal = dto.Nominal,
            Proportion = dto.Proportion
        };
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }


        var dtos = TransactionData.GetInitialTransactions();
        var transactions = new List<Transaction>();
        foreach (var dto in dtos)
        {
            var transaction = await Map(dto);
            transactions.Add(transaction);
        }

        await _transactionRepository.AddTransactionsAsync(transactions);
    }
}