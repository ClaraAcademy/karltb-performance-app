using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Queries;
using PerformanceApp.Data.Seeding.Utilities;

namespace PerformanceApp.Data.Seeding.Entities;

public class InstrumentPerformanceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly InstrumentPerformanceRepository _instrumentPerformanceRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var instrumentPerformances = await _instrumentPerformanceRepository.GetInstrumentPerformancesAsync();

        return instrumentPerformances.Any();
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
            var query = PerformanceQueries.UpdateInstrumentDayPerformance(bankday);
            await SqlExecutor.ExecuteQueryAsync(_context, query);
        }


    }
}