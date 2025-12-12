using PerformanceApp.Data.Context.Configuration.Constants.Indexes.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Indexes;

public static class IndexTransactionType
{
    private static readonly IndexFactory _factory = new(nameof(TransactionType));

    public static string Name => _factory.Name(nameof(TransactionType.Name));
}