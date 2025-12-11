using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Context.Configuration.Constants.Entities;

public class BenchmarkConstants : EntityConstants<Benchmark>
{
    private const string _benchmarkPortfolioForeignKey = "FK_Benchmark_BenchmarkID";
    private const string _portfolioPortfolioForeignKey = "FK_Benchmark_PortfolioID";

    public const string BenchmarkPortfolioForeignKey = _benchmarkPortfolioForeignKey;
    public const string PortfolioPortfolioForeignKey = _portfolioPortfolioForeignKey;
}