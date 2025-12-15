using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Indexes;

public static class IndexTransactionType
{
    private static readonly IndexFactory _factory = new(nameof(TransactionType));

    public static string Name => _factory.Name(nameof(TransactionType.Name));
}