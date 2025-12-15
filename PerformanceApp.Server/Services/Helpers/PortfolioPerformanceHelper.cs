using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Server.Services.Helpers;

public static class PortfolioPerformanceHelper
{
    public static bool IsCumulativeDayPerformance(PortfolioPerformance portfolioPerformance)
    {
        var performanceTypeName = portfolioPerformance.PerformanceTypeNavigation.Name;

        return performanceTypeName == PerformanceTypeData.CumulativeDayPerformance;
    }

    public static DateOnly GetBankday(PortfolioPerformanceDTO portfolioPerformanceDTO)
    {
        return portfolioPerformanceDTO.Bankday;
    }

    public static List<PortfolioBenchmarkPerformanceDTO> Join(
        IEnumerable<PortfolioPerformanceDTO> lhs,
        IEnumerable<PortfolioPerformanceDTO> rhs
    )
    {
        var joined = 
            from lp in lhs
            join rp in rhs
            on GetBankday(lp) equals GetBankday(rp)
            select PortfolioPerformanceMapper.MapToPortfolioBenchmarkPerformanceDTO(lp, rp);

        return joined.ToList();
    }
}