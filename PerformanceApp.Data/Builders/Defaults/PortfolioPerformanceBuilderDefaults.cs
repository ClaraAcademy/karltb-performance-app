using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class PortfolioPerformanceBuilderDefaults
{
    public static int Id => 0;
    public static int PortfolioId => 1;
    public static int PerformanceTypeId => 1;
    public static decimal Value => 1000.00m;
    public static DateOnly PeriodStart => DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1));
    public static DateOnly PeriodEnd => DateOnly.FromDateTime(DateTime.UtcNow);

    public static PortfolioPerformance PortfolioPerformance => new PortfolioPerformanceBuilder().Build();
}