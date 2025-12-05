using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Services;
using PerformanceApp.Data.Seeding.Utilities;

namespace PerformanceApp.Data.Seeding.Entities;

public class PortfolioPerformanceSeeder(PadbContext context)
{
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly PortfolioPerformanceRepository _portfolioPerformanceRepository = new(context);
    private readonly IPortfolioPerformanceService _portfolioPerformanceService = new PortfolioPerformanceService(context);

    private async Task<bool> IsPopulated()
    {
        var portfolioPerformances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();
        return portfolioPerformances.Any();
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();
        var bankdays = BankdayHelper.GetOrderedBankdays(dateInfos);

        foreach (var bankday in bankdays)
        {
            await _portfolioPerformanceService.UpdatePortfolioDayPerformancesAsync(bankday);
        }

        foreach (var bankday in bankdays)
        {
            await _portfolioPerformanceService.UpdatePortfolioCumulativeDayPerformancesAsync(bankday);
        }

        var months = dateInfos
            .Select(di => new DateOnly(di.Bankday.Year, di.Bankday.Month, 1))
            .Distinct()
            .OrderBy(d => d);

        foreach (var month in months)
        {
            await _portfolioPerformanceService.UpdatePortfolioMonthPerformancesAsync(month);
        }

        var halfYears = dateInfos
            .Where(di => di.Bankday.Year > 2016)
            .Select(di => new DateOnly(di.Bankday.Year, di.Bankday.Month <= 6 ? 1 : 7, 1))
            .Distinct()
            .OrderBy(d => d);

        foreach (var halfYear in halfYears)
        {
            await _portfolioPerformanceService.UpdatePortfolioHalfYearPerformancesAsync(halfYear);
        }
    }
}