using PerformanceApp.Data.Constants;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Helpers;

public static class PortfolioPerformanceHelper
{
    public static bool IsCumulativeDayPerformance(PortfolioPerformance portfolioPerformance)
    {
        var name = portfolioPerformance.PerformanceTypeNavigation.Name;

        return name == PerformanceTypeConstants.CumulativeDay;
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