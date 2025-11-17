using PerformanceApp.Data.Seeding.Dtos;

namespace PerformanceApp.Data.Seeding.Constants;

public static class TransactionData
{
    private static readonly TransactionDto PortfolioA_SsabB = new(
        PortfolioData.PortfolioA,
        InstrumentData.SsabB,
        BankdayData.FirstDay,
        Count: TransactionWeights.Count_PortfolioA_SsabB
    );
    private static readonly TransactionDto PortfolioA_AstraZeneca = new(
        PortfolioData.PortfolioA,
        InstrumentData.AstraZeneca,
        BankdayData.FirstDay,
        Count: TransactionWeights.Count_PortfolioA_AstraZeneca
    );
    private static List<TransactionDto> GetPortfolioA_Transactions()
    {
        return [PortfolioA_SsabB, PortfolioA_AstraZeneca];
    }

    private static readonly TransactionDto PortfolioB_SsabB = new(
        PortfolioData.PortfolioB,
        InstrumentData.SsabB,
        BankdayData.FirstDay,
        Count: TransactionWeights.Count_PortfolioB_SsabB
    );
    private static readonly TransactionDto PortfolioB_AstraZeneca = new(
        PortfolioData.PortfolioB,
        InstrumentData.AstraZeneca,
        BankdayData.FirstDay,
        Count: TransactionWeights.Count_PortfolioB_AstraZeneca
    );
    private static readonly TransactionDto PortfolioB_Statsobligation1046 = new(
        PortfolioData.PortfolioB,
        InstrumentData.Statsobligation1046,
        BankdayData.FirstDay,
        Nominal: TransactionWeights.Nominal_PortfolioB_Statsobligation1046
    );
    private static List<TransactionDto> GetPortfolioB_Transactions()
    {
        return [PortfolioB_SsabB, PortfolioB_AstraZeneca, PortfolioB_Statsobligation1046];
    }

    private static readonly TransactionDto BenchmarkA_Omx30 = new(
        PortfolioData.BenchmarkA,
        InstrumentData.Omx30,
        BankdayData.FirstDay,
        Proportion: TransactionWeights.Proportion_BenchmarkA_Omx30
    );
    private static List<TransactionDto> GetBenchmarkA_Transactions()
    {
        return [BenchmarkA_Omx30];
    }

    private static readonly TransactionDto BenchmarkB_Omx30 = new(
        PortfolioData.BenchmarkB,
        InstrumentData.Omx30,
        BankdayData.FirstDay,
        Proportion: TransactionWeights.Proportion_BenchmarkB_Omx30
    );
    private static readonly TransactionDto BenchmarkB_OmrXtBond = new(
        PortfolioData.BenchmarkB,
        InstrumentData.OmrXtBond,
        BankdayData.FirstDay,
        Proportion: TransactionWeights.Proportion_BenchmarkB_OmrXtBond
    );
    private static List<TransactionDto> GetBenchmarkB_Transactions()
    {
        return [BenchmarkB_Omx30, BenchmarkB_OmrXtBond];
    }

    public static List<TransactionDto> GetInitialTransactions()
    {
        var portfolioA = GetPortfolioA_Transactions();
        var portfolioB = GetPortfolioB_Transactions();
        var benchmarkA = GetBenchmarkA_Transactions();
        var benchmarkB = GetBenchmarkB_Transactions();

        return [.. portfolioA, .. portfolioB, .. benchmarkA, .. benchmarkB];
    }


}