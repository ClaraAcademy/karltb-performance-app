using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Data.Seeding.Queries;

public static class TransactionQueries
{
    public static FormattableString BuyInstrument(TransactionDto transaction)
    {
        var portfolioName = transaction.PortfolioName;
        var instrumentName = transaction.InstrumentName;
        var count = transaction.Count;
        var amount = transaction.Amount;
        var proportion = transaction.Proportion;
        var nominal = transaction.Nominal;
        var date = transaction.Bankday;
        return BuyInstrument(portfolioName, instrumentName, date, count, amount, proportion, nominal);
    }
    public static FormattableString BuyInstrument(string portfolioName, string instrumentName, DateOnly date, int? count = null, decimal? amount = null, decimal? proportion = null, decimal? nominal = null)
    {
        return $@"EXEC [padb].[uspBuyInstrument]
            @PortfolioName = {portfolioName},
            @InstrumentName = {instrumentName},
            @Count = {count},
            @Amount = {amount},
            @Proportion = {proportion},
            @Nominal = {nominal},
            @BuyDate = {date}";
    }
}