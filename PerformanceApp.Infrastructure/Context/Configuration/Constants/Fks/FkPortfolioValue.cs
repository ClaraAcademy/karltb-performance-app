using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Infrastructure.Context.Configuration.Constants.Fks;

public static class FkPortfolioValue
{
    private static readonly FkFactory _factory = new(nameof(PortfolioValue));

    public static string Bankday => _factory.Name(nameof(PortfolioValue.Bankday));
    public static string PortfolioId => _factory.Name(nameof(PortfolioValue.PortfolioId));
}