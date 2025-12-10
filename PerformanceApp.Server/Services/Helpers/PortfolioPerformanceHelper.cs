using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Server.Dtos;

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
}