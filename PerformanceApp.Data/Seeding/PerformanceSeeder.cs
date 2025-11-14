using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Seeding;

public class PerformanceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private List<FormattableString> GetDailyQueries(DateOnly bankday)
    {
        return [
            $@"EXEC [padb].[uspUpdatePositions] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioValue] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdateInstrumentDayPerformance] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioDayPerformance] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioCumulativeDayPerformance] @Bankday = {bankday};"
        ];
    }

    private List<FormattableString> GetPerformanceQueries()
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

    public void Seed()
    {
        var bankdays = _dateInfoRepository.GetDateInfos()
            .Select(di => di.Bankday)
            .OrderBy(d => d)
            .ToList();

        foreach (var bankday in bankdays)
        {
            foreach (var query in GetDailyQueries(bankday))
            {
                _context.Database.ExecuteSqlInterpolated(query);
                _context.SaveChanges();
            }
        }

        foreach (var query in GetPerformanceQueries())
        {
            _context.Database.ExecuteSqlInterpolated(query);
            _context.SaveChanges();
        }
    }
}