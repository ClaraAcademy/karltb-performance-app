using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services.Mappers;

namespace PerformanceApp.Server.Services.Helpers;

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