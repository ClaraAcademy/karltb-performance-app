using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Entities;

public class PerformanceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private static List<FormattableString> GetDailyQueries(DateOnly bankday)
    {
        return [
            PerformanceQueries.UpdatePortfolioValue(bankday),
            PerformanceQueries.UpdateInstrumentDayPerformance(bankday),
            PerformanceQueries.UpdatePortfolioDayPerformance(bankday),
            PerformanceQueries.UpdatePortfolioCumulativeDayPerformance(bankday)
        ];
    }

    private static List<FormattableString> GetAggregatePerformanceQueries()
    {
        return [
            PerformanceQueries.UpdatePortfolioMontPerformance(),
            PerformanceQueries.UpdatePortfolioHalfYearPerformance()
        ];
    }

    private static DateOnly GetBankday(DateInfo dateInfo) => dateInfo.Bankday;

    public async Task Seed()
    {
        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();
        var bankdays = dateInfos.Select(GetBankday)
            .OrderBy(d => d)
            .ToList();

        foreach (var bankday in bankdays)
        {
            foreach (var query in GetDailyQueries(bankday))
            {
                await SqlExecutor.ExecuteQueryAsync(_context, query);
            }
        }

        foreach (var query in GetAggregatePerformanceQueries())
        {
            await SqlExecutor.ExecuteQueryAsync(_context, query);
        }
    }
}