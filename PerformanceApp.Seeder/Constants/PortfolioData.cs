namespace PerformanceApp.Seeder.Constants;

public static class PortfolioData
{
    public static readonly string PortfolioA = "Portfolio A";
    public static readonly string PortfolioB = "Portfolio B";
    public static readonly string BenchmarkA = "Benchmark A";
    public static readonly string BenchmarkB = "Benchmark B";

    private static readonly List<string> _allPortfolios = [
        PortfolioA,
        PortfolioB,
        BenchmarkA,
        BenchmarkB
    ];

    private static readonly List<string> _portfolios = [
        PortfolioA,
        PortfolioB
    ];

    private static readonly List<string> _benchmarks = [
        BenchmarkA,
        BenchmarkB
    ];

    public static List<string> AllPortfolios => _allPortfolios.OrderBy(n => n).ToList();
    public static List<string> Portfolios => _portfolios.OrderBy(n => n).ToList();
    public static List<string> Benchmarks => _benchmarks.OrderBy(n => n).ToList();
}