using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Queries;
using PerformanceApp.Data.Seeding.Dtos;
using PerformanceApp.Data.Seeding.Utilities;

namespace PerformanceApp.Data.Seeding.Entities;

public class TransactionSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private async Task<List<FormattableString>> GetBuyQueries()
    {
        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();

        DateOnly firstDay = BankdayHelper.GetFirstBankday(dateInfos);

        var transactions = new List<TransactionDto>()
        {
            new(PortfolioData.PortfolioA, InstrumentData.SsabB, firstDay, Count: TransactionWeights.CountSsabbPortfolioA),
            new(PortfolioData.PortfolioA, InstrumentData.AstraZeneca, firstDay, Count: TransactionWeights.CountAstraZenecaPortfolioA),
            new(PortfolioData.PortfolioB, InstrumentData.SsabB, firstDay, Count: TransactionWeights.CountSsabbPortfolioB),
            new(PortfolioData.PortfolioB, InstrumentData.AstraZeneca, firstDay, Count: TransactionWeights.CountAstraZenecaPortfolioB),
            new(PortfolioData.PortfolioB, InstrumentData.Statsobligation1046, firstDay, Nominal: TransactionWeights.NominalStatsobligation1046PortfolioB),
            new(PortfolioData.BenchmarkA, InstrumentData.Omx30, firstDay, Proportion: TransactionWeights.ProportionOmx30BenchmarkA),
            new(PortfolioData.BenchmarkB, InstrumentData.Omx30, firstDay, Proportion: TransactionWeights.ProportionOmx30BenchmarkB),
            new(PortfolioData.BenchmarkB, InstrumentData.OmrXtBond, firstDay, Proportion: TransactionWeights.ProportionOmrXtBondBenchmarkB)
        };

        return transactions.Select(TransactionQueries.BuyInstrument).ToList();
    }

    public async Task Seed()
    {
        var queries = await GetBuyQueries();

        foreach (var q in queries)
        {
            await SqlExecutor.ExecuteQueryAsync(_context, q);
        }
    }
}