using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class PortfolioBuilderDefaults
{
    public static int PortfolioId => 1;
    public static string PortfolioName => "Default Portfolio";
    public static Portfolio Portfolio => new PortfolioBuilder().Build();

    public static int BenchmarkId => 100;
    public static string BenchmarkName => "Default Benchmark";
    public static Portfolio Benchmark => new PortfolioBuilder()
        .WithName(BenchmarkName)
        .Build();
}