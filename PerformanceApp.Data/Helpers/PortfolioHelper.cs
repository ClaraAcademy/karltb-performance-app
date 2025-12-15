using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Helpers;

public static class PortfolioHelper
{
    public static IEnumerable<PortfolioPerformanceDTO> GetCumulativeDayPerformanceDtos(this Portfolio portfolio)
    {
        return portfolio
            .PortfolioPerformancesNavigation
            .Where(PortfolioPerformanceHelper.IsCumulativeDayPerformance)
            .Select(PortfolioPerformanceMapper.MapToPortfolioPerformanceDTO);
    }

}