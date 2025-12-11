using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkPortfolioPerformance
{
    private static readonly FkFactory _factory = new(nameof(PortfolioPerformance));

    public static string PortfolioId => _factory.Name(nameof(PortfolioPerformance.PortfolioId));
    public static string TypeId => _factory.Name(nameof(PortfolioPerformance.TypeId));
    public static string PeriodStart => _factory.Name(nameof(PortfolioPerformance.PeriodStart));
    public static string PeriodEnd => _factory.Name(nameof(PortfolioPerformance.PeriodEnd));
}