using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Queries;
using PerformanceApp.Data.Seeding.Utilities;

namespace PerformanceApp.Data.Seeding.Entities;

public class PortfolioValueSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly PortfolioValueRepository _portfolioValueRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var portfolioValues = await _portfolioValueRepository.GetPortfolioValuesAsync();

        return portfolioValues.Any();
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
            var query = PerformanceQueries.UpdatePortfolioValue(bankday);
            await SqlExecutor.ExecuteQueryAsync(_context, query);
        }
    }
}