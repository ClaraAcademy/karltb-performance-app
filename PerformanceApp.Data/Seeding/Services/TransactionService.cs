using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Services;

public interface ITransactionService
{
    Task<bool> BuyInstrumentAsync(Portfolio portfolio, Instrument instrument, DateOnly date, int? count, decimal? amount, decimal? proportion, decimal? nominal);
}

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IInstrumentService _instrumentService;
    private readonly TransactionType _buyTransactionType;

    public TransactionService(PadbContext context)
    {
        _transactionRepository = new TransactionRepository(context);
        _instrumentService = new InstrumentService(context);
        _buyTransactionType = new TransactionTypeRepository(context)
            .GetTransactionTypesAsync()
            .Result
            .First(tt => tt.Name == TransactionTypeData.Buy);
    }

    public async Task<bool> BuyInstrumentAsync(Portfolio portfolio, Instrument instrument, DateOnly date, int? count, decimal? amount, decimal? proportion, decimal? nominal)
    {
        var correctWeight = await _instrumentService.InstrumentHasCorrectWeight(instrument.Id, count, amount, proportion, nominal);
        if (!correctWeight)
        {
            return false;
        }

        var transaction = new Transaction
        {
            PortfolioId = portfolio.Id,
            InstrumentId = instrument.Id,
            Bankday = date,
            Count = count,
            Amount = amount,
            Proportion = proportion,
            Nominal = nominal,
            TransactionTypeId = _buyTransactionType.Id,
            TransactionTypeNavigation = _buyTransactionType
        };

        await _transactionRepository.AddTransactionAsync(transaction);
        return true;
    }

}
