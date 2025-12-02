using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface ITransactionService
{
    Task<bool> BuyInstrumentAsync(Portfolio portfolio, Instrument instrument, DateOnly date, int? count, decimal? amount, decimal? proportion, decimal? nominal);
}

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionTypeRepository _transactionTypeRepository;
    private readonly IInstrumentService _instrumentService;
    private readonly TransactionType _buyTransactionType;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ITransactionTypeRepository transactionTypeRepository,
        IInstrumentService instrumentService
    )
    {
        _transactionRepository = transactionRepository;
        _transactionTypeRepository = transactionTypeRepository;
        _instrumentService = instrumentService;
        _buyTransactionType = _transactionTypeRepository
            .GetTransactionTypesAsync()
            .Result
            .First(tt => tt.Name == "Buy");
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
