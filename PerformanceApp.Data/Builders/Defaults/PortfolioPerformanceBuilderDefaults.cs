namespace PerformanceApp.Data.Builders.Defaults;

public static class PortfolioPerformanceBuilderDefaults
{
    public static decimal Value => 1000.00m;
    public static DateOnly PeriodStart => DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-1));
    public static DateOnly PeriodEnd => DateOnly.FromDateTime(DateTime.UtcNow);
}