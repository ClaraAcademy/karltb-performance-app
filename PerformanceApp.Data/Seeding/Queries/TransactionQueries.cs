using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Data.Seeding.Queries;

public static class TransactionQueries
{
    private static FormattableString GetQuery(TransactionDto transaction)
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
    private static FormattableString BuyInstrument(string portfolioName, string instrumentName, DateOnly date, int? count = null, decimal? amount = null, decimal? proportion = null, decimal? nominal = null)
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

    public static List<FormattableString> GetBuyQueries()
    {
        var transactions = TransactionData.GetInitialTransactions();
        return transactions.Select(GetQuery).ToList();
    }
}