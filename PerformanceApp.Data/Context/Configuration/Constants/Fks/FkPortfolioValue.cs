using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkPortfolioValue
{
    private static readonly FkFactory _factory = new(nameof(PortfolioValue));

    public static string Bankday => _factory.Name(nameof(PortfolioValue.Bankday));
    public static string PortfolioId => _factory.Name(nameof(PortfolioValue.PortfolioId));
}