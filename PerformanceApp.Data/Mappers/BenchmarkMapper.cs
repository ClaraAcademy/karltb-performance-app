using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Mappers;

public static class BenchmarkMapper
{
    public static PortfolioBenchmarkDTO MapToPortfolioBenchmarkDTO(Benchmark benchmarkMapping)
    {
        var portfolioId = benchmarkMapping.PortfolioPortfolioNavigation.Id;
        var portfolioName = benchmarkMapping.PortfolioPortfolioNavigation.Name;
        var benchmarkId = benchmarkMapping.BenchmarkPortfolioNavigation.Id;
        var benchmarkName = benchmarkMapping.BenchmarkPortfolioNavigation.Name;

        return new PortfolioBenchmarkDTO
        {
            PortfolioId = portfolioId,
            PortfolioName = portfolioName,
            BenchmarkId = benchmarkId,
            BenchmarkName = benchmarkName
        };
    }

    public static Benchmark Map(Portfolio portfolio, Portfolio benchmark)
    {
        return new Benchmark
        {
            PortfolioId = portfolio.Id,
            BenchmarkId = benchmark.Id,
            PortfolioPortfolioNavigation = portfolio,
            BenchmarkPortfolioNavigation = benchmark
        };
    }

    public static Benchmark Map((Portfolio, Portfolio) pair)
    {
        var (portfolio, benchmark) = pair;
        return Map(portfolio, benchmark);
    }

}