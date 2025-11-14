using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Seeding.Entities;

public class PositionSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);

    private static async Task<FormattableString> GetBuyQuery(string portfolioName, string instrumentName, DateOnly date, int? count = null, decimal? amount = null, decimal? proportion = null, decimal? nominal = null)
    {
        return $@"EXEC [padb].[uspBuyInstrument]
            @PortfolioName = {portfolioName},
            @InstrumentName = {instrumentName},
            @Count = {count},
            @Amount = {amount},
            @Proportion = {proportion},
            @Nominal = {nominal},
            @BuyDate = {date}";
    }

    private async Task<List<FormattableString>> GetBuyQueries()
    {
        DateOnly firstDay = _dateInfoRepository
            .GetDateInfos()
            .Select(di => di.Bankday)
            .Min();

        var portfolioA = "Portfolio A";
        var portfolioB = "Portfolio B";
        var benchmarkA = "Benchmark A";
        var benchmarkB = "Benchmark B";

        var ssabB = "SSAB B";
        var astraZeneca = "Astra Zeneca";
        var statsobligation1046 = "Statsobligation 1046";
        var omx30 = "OMX30";
        var omrXtBond = "OMRXTBOND";

        return
        [
            await GetBuyQuery(portfolioA, ssabB, firstDay, count : 40000),
            await GetBuyQuery(portfolioA, astraZeneca, firstDay, count : 13200),
            await GetBuyQuery(portfolioB, ssabB, firstDay, count : 20000),
            await GetBuyQuery(portfolioB, astraZeneca, firstDay, count : 6600),
            await GetBuyQuery(portfolioB, statsobligation1046, firstDay, nominal : 5000000.0m),
            await GetBuyQuery(benchmarkA, omx30, firstDay, proportion : 1.0m),
            await GetBuyQuery(benchmarkB, omx30, firstDay, proportion : 0.5m),
            await GetBuyQuery(benchmarkB, omrXtBond, firstDay, proportion : 0.5m)
        ];

    }

    public async Task Seed()
    {
        var queries = await GetBuyQueries();

        foreach (var q in queries)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(q);
        }

        await _context.SaveChangesAsync();
    }
}