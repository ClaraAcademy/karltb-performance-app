using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PerformanceApp.Data.Seeding;

public class PerformanceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private static List<FormattableString> GetDailyQueries(DateOnly bankday)
    {
        return [
            $@"EXEC [padb].[uspUpdatePositions] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioValue] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdateInstrumentDayPerformance] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioDayPerformance] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioCumulativeDayPerformance] @Bankday = {bankday};"
        ];
    }

    private static List<FormattableString> GetPerformanceQueries()
    {
        return [
            $@"EXEC padb.uspUpdatePortfolioMonthPerformance;",
            $@"EXEC padb.uspUpdatePortfolioHalfYearPerformance;",
            $@"EXEC padb.uspUpdateStandardDeviation;",
            $@"EXEC padb.uspUpdateTrackingError;",
            $@"EXEC padb.uspUpdateAnnualisedCumulativeReturn;",
            $@"EXEC padb.uspUpdateInformationRatio;",
            $@"EXEC padb.uspUpdateHalfYearPerformance;"
        ];
    }

    private static DateOnly GetBankday(DateInfo dateInfo) => dateInfo.Bankday;

    public async Task Seed()
    {
        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();
        var bankdays = dateInfos
            .Select(GetBankday)
            .OrderBy(d => d)
            .ToList();

        foreach (var bankday in bankdays)
        {
            foreach (var query in GetDailyQueries(bankday))
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(query);
                await _context.SaveChangesAsync();
            }
        }

        foreach (var query in GetPerformanceQueries())
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(query);
            await _context.SaveChangesAsync();
        }
    }
}