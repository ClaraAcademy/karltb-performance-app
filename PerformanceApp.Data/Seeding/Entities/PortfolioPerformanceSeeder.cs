using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Utilities;

namespace PerformanceApp.Data.Seeding.Entities;

public class PortfolioPerformanceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;

    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly PortfolioPerformanceRepository _portfolioPerformanceRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var portfolioPerformances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();
        return portfolioPerformances.Any();
    }

    private static List<FormattableString> GetDailyQueries(DateOnly bankday)
    {
        return [
            PerformanceQueries.UpdatePortfolioDayPerformance(bankday),
            PerformanceQueries.UpdatePortfolioCumulativeDayPerformance(bankday)
        ];
    }

    private static List<FormattableString> GetAggregateQueries()
    {
        return [
            PerformanceQueries.UpdatePortfolioMontPerformance(),
            PerformanceQueries.UpdatePortfolioHalfYearPerformance()
        ];
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
            foreach (var query in GetDailyQueries(bankday))
            {
                await SqlExecutor.ExecuteQueryAsync(_context, query);
            }
        }

        foreach (var query in GetAggregateQueries())
        {
            await SqlExecutor.ExecuteQueryAsync(_context, query);
        }
    }
}