using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using Microsoft.Data.SqlClient;

namespace PerformanceApp.Data.Seeding.Entities;

public class PerformanceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private static List<FormattableString> GetDailyQueries(DateOnly bankday)
    {
        var bankdayParameter = new SqlParameter("Bankday", bankday);
        return [
            $@"EXEC [padb].[uspUpdatePositions] @Bankday = {bankdayParameter};",
            $@"EXEC [padb].[uspUpdatePortfolioValue] @Bankday = {bankdayParameter};",
            $@"EXEC [padb].[uspUpdateInstrumentDayPerformance] @Bankday = {bankdayParameter};",
            $@"EXEC [padb].[uspUpdatePortfolioDayPerformance] @Bankday = {bankdayParameter};",
            $@"EXEC [padb].[uspUpdatePortfolioCumulativeDayPerformance] @Bankday = {bankdayParameter};"
        ];
    }

    private static List<FormattableString> GetPerformanceQueries()
    {
        return [
            $@"EXEC padb.uspUpdatePortfolioMonthPerformance;",
            $@"EXEC padb.uspUpdatePortfolioHalfYearPerformance;",
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

        foreach (var query in GetPerformanceQueries())
        {
            await SqlExecutor.ExecuteQueryAsync(_context, query);
        }
    }
}