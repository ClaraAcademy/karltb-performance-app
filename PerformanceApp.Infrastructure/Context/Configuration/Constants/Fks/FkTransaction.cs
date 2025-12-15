using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkTransaction
{
    private static readonly FkFactory _factory = new(nameof(Transaction));

    public static string Bankday => _factory.Name(nameof(Transaction.Bankday));
    public static string InstrumentId => _factory.Name(nameof(Transaction.InstrumentId));
    public static string PortfolioId => _factory.Name(nameof(Transaction.PortfolioId));
    public static string TransactionTypeId => _factory.Name(nameof(Transaction.TransactionTypeId));

}