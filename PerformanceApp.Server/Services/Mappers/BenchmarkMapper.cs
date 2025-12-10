using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Services.Mappers;

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

}