namespace PerformanceApp.Data.Seeding.Constants;

public static class TransactionTypeData
{
    public static readonly string Buy = "Buy";
    public static readonly string Sell = "Sell";

    public static List<string> GetTransactionTypes()
    {
        return [Buy, Sell];
    }
}