using PerformanceApp.Data.Constants;

namespace PerformanceApp.Seeder.Constants;

public static class TransactionTypeData
{
    private static readonly List<string> _transactionTypes = [TransactionTypeConstants.Buy, TransactionTypeConstants.Sell];

    public static List<string> TransactionTypes => _transactionTypes.OrderBy(n => n).ToList();
}