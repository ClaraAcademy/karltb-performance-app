namespace PerformanceApp.Data.Builders.Defaults;

public static class PortfolioValueBuilderDefaults
{
    public static readonly int PortfolioId = PortfolioBuilderDefaults.PortfolioId;
    public const decimal Value = 1000m;
    public static readonly DateOnly Bankday = DateOnly.FromDateTime(DateTime.UtcNow.Date);
}