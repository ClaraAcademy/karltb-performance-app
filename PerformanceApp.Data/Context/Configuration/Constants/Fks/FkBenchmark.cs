using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context.Configuration.Constants.Fks.Factory;

namespace PerformanceApp.Data.Context.Configuration.Constants.Fks;

public static class FkBenchmark
{
    private static readonly FkFactory _factory = new FkFactory(nameof(Benchmark));

    public static string BenchmarkPortfolio => _factory.Name(nameof(Benchmark.BenchmarkId));
    public static string PortfolioPortfolio => _factory.Name(nameof(Benchmark.PortfolioId));
}