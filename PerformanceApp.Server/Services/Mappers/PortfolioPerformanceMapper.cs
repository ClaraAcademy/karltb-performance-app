using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Models;

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

    public static List<DataPoint2> MapToDataPoint2s(
        IEnumerable<PortfolioBenchmarkPerformanceDTO> dtos
    )
    {
        return dtos
            .Select(MapToDataPoint2)
            .ToList();
    }

    public static DataPoint2 MapToDataPoint2(PortfolioBenchmarkPerformanceDTO dto)
    {
        var x = dto.Bankday;
        var y1 = (float)dto.PortfolioValue;
        var y2 = (float)dto.BenchmarkValue;

        return new(x, y1, y2);
    }



}