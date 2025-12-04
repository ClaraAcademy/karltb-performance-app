namespace PerformanceApp.Data.Seeding.Constants;

public static class TransactionTypeData
{
    public static readonly string Buy = "Buy";
    public static readonly string Sell = "Sell";

    private static readonly List<string> _transactionTypes = [Buy, Sell];

    public static List<string> TransactionTypes => _transactionTypes.OrderBy(n => n).ToList();

    public static List<string> GetTransactionTypes()
    {
        return [Buy, Sell];
    }
}