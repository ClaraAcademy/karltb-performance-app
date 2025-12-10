using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Services.Mappers;

public static class PortfolioPerformanceMapper
{
    public static PortfolioPerformanceDTO MapToPortfolioPerformanceDTO(PortfolioPerformance portfolioPerformance)
    {
        var bankday = portfolioPerformance.PeriodStart;
        var value = portfolioPerformance.Value;

        return MapToPortfolioPerformanceDTO(bankday, value);
    }

    private static PortfolioPerformanceDTO MapToPortfolioPerformanceDTO(DateOnly bankday, decimal value)
    {
        return new PortfolioPerformanceDTO { Bankday = bankday, Value = value };
    }

    public static PortfolioBenchmarkPerformanceDTO MapToPortfolioBenchmarkPerformanceDTO(
        PortfolioPerformanceDTO portfolioPerformance, PortfolioPerformanceDTO benchmarkPerformance
    )
    {
        var bankday = portfolioPerformance.Bankday;
        var portfolioValue = portfolioPerformance.Value;
        var benchmarkValue = benchmarkPerformance.Value;

        return MapToPortfolioBenchmarkCumulativeDayPerformanceDTO(
            bankday, portfolioValue, benchmarkValue
        );
    }

    private static PortfolioBenchmarkPerformanceDTO MapToPortfolioBenchmarkCumulativeDayPerformanceDTO(
        DateOnly bankday, decimal portfolioValue, decimal benchmarkValue
    )
    {
        return new PortfolioBenchmarkPerformanceDTO
        {
            Bankday = bankday,
            PortfolioValue = portfolioValue,
            BenchmarkValue = benchmarkValue
        };
    }



}