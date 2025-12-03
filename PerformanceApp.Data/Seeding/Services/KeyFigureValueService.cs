using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IKeyFigureValueService
{
    Task<bool> UpdateStandardDeviationAsync();
    Task<bool> UpdateTrackingErrorAsync();
    Task<bool> UpdateAnnualisedCumulativeReturnAsync();
    Task<bool> UpdateInformationRatioAsync();
    Task<bool> UpdateHalfYearPerformanceAsync();
}

public class KeyFigureValueService(PadbContext context) : IKeyFigureValueService
{
    private readonly IKeyFigureValueRepository _keyFigureValueRepository = new KeyFigureValueRepository(context);
    private readonly IKeyFigureInfoService _keyFigureInfoService = new KeyFigureInfoService(context);
    private readonly IPortfolioRepository _portfolioRepository = new PortfolioRepository(context);
    private readonly IPortfolioPerformanceService _portfolioPerformanceService = new PortfolioPerformanceService(context);
    private readonly IPortfolioPerformanceRepository _portfolioPerformanceRepository = new PortfolioPerformanceRepository(context);
    private readonly decimal AnnualizationFactor = new DateInfoService(context)
            .GetAnnualizationFactorAsync()
            .GetAwaiter()
            .GetResult();
    private readonly int DayPerformanceId = new PerformanceService(context)
            .GetDayPerformanceIdAsync()
            .GetAwaiter()
            .GetResult();

    private record Dto(int PortfolioId, int KeyFigureId, decimal Value);
    private static Dto Aggregate(IGrouping<int, PortfolioPerformance> group, int KeyFigureId, Func<IEnumerable<decimal>, decimal> func)
    {
        var values = group.Select(pp => pp.Value);
        return new Dto(group.Key, KeyFigureId, func(values));
    }
    private static Dto Aggregate(IGrouping<int, Dto> group, int KeyFigureId)
    {
        return new Dto
        (
            group.Key,
            KeyFigureId,
            ComputeStandardDeviation(
                group.Select(dto => dto.Value)
            )
        );
    }
    private static Dto Aggregate(IGrouping<int, Dto> group, int KeyFigureId, Func<IEnumerable<decimal>, decimal> func)
    {
        var portfolioId = group.Key;
        var values = group.Select(dto => dto.Value);
        return new Dto(portfolioId, KeyFigureId, func(values));
    }

    private static KeyFigureValue MapToKeyFigureValue(int portfolioId, int keyFigureId, decimal value)
    {
        return new KeyFigureValue
        {
            PortfolioId = portfolioId,
            KeyFigureId = keyFigureId,
            Value = value
        };
    }
    private static KeyFigureValue MapToKeyFigureValue(Dto dto)
    {
        return MapToKeyFigureValue(dto.PortfolioId, dto.KeyFigureId, dto.Value);
    }

    private static decimal ComputeStandardDeviation(IEnumerable<decimal> values)
    {
        var mean = values.Average();
        var n = values.Count();

        if (n < 2)
        {
            return 0M;
        }

        var sum = values
            .Select(v => v - mean)
            .Select(v => v * v)
            .Sum();

        var fraction = (double)sum / (n - 1);

        return (decimal)Math.Sqrt(fraction);
    }

    public async Task<bool> UpdateStandardDeviationAsync()
    {
        var id = await _keyFigureInfoService.GetStandardDeviationIdAsync();
        var dayPerformances = await _portfolioPerformanceService.GetPortfolioDayPerformancesAsync();

        var keyFigureValues = dayPerformances
            .GroupBy(pp => pp.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeStandardDeviation))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(keyFigureValues);
        return true;
    }

    private IEnumerable<PortfolioPerformance> GetPortfolioDayPerformances(Portfolio portfolio)
    {
        return portfolio.PortfolioPerformancesNavigation
            .Where(pp => pp.TypeId == DayPerformanceId);
    }

    private List<Dto> GetActiveReturn(Portfolio portfolio)
    {
        var benchmark = portfolio.BenchmarkPortfoliosNavigation
            .Select(b => b.BenchmarkPortfolioNavigation)
            .FirstOrDefault();

        if (benchmark == null)
        {
            return [];
        }

        var portfolioDayPerformances = GetPortfolioDayPerformances(portfolio);
        var benchmarkDayPerformances = GetPortfolioDayPerformances(benchmark);

        static DateOnly GetKey(PortfolioPerformance pp) => pp.PeriodStart;
        static Dto Map(PortfolioPerformance pp, PortfolioPerformance bp)
        {
            return new(pp.PortfolioId, -1, pp.Value - bp.Value);
        }

        var dtos = portfolioDayPerformances
            .Join(benchmarkDayPerformances, GetKey, GetKey, Map)
            .ToList();

        return dtos;
    }

    private List<Dto> GetActiveReturns(List<Portfolio> portfolios)
    {
        return portfolios.SelectMany(GetActiveReturn).ToList();
    }

    public async Task<bool> UpdateTrackingErrorAsync()
    {
        var id = await _keyFigureInfoService.GetTrackingErrorIdAsync();

        var actualPortfolios = await _portfolioRepository.GetProperPortfoliosAsync();
        var portfolios = actualPortfolios.ToList();

        var activeReturns = GetActiveReturns(portfolios);

        var trackingErrors = activeReturns
            .GroupBy(ar => ar.PortfolioId)
            .Select(g => Aggregate(g, id))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(trackingErrors);
        return true;
    }

    private static decimal Product(decimal acc, decimal r) => acc * (1M + r);

    public async Task<bool> UpdateAnnualisedCumulativeReturnAsync()
    {
        var id = await _keyFigureInfoService.GetAnnualizedCumulativeReturnIdAsync();

        var dayPerformances = await _portfolioPerformanceService.GetPortfolioDayPerformancesAsync();

        decimal ComputeAnnualizedCumulativeReturn(IEnumerable<decimal> dailyReturns)
        {
            var product = dailyReturns.Aggregate(1M, Product);

            return (decimal)Math.Pow((double)product, (double)AnnualizationFactor) - 1M;
        }

        var annualisedCumulativeReturns = dayPerformances
            .GroupBy(pp => pp.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeAnnualizedCumulativeReturn))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(annualisedCumulativeReturns);
        return true;
    }

    public async Task<bool> UpdateInformationRatioAsync()
    {
        var id = await _keyFigureInfoService.GetInformationRatioIdAsync();

        var actualPortfolios = await _portfolioRepository.GetProperPortfoliosAsync();
        var portfolios = actualPortfolios.ToList();

        var activeReturns = GetActiveReturns(portfolios);

        decimal ComputeInformationRatio(IEnumerable<decimal> activeReturns)
        {
            var average = activeReturns.Average();
            var stdDev = ComputeStandardDeviation(activeReturns);

            var factor = (decimal)Math.Sqrt((double)AnnualizationFactor);

            return factor * average / stdDev;
        }

        var informationRatios = activeReturns
            .GroupBy(ar => ar.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeInformationRatio))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(informationRatios);
        return true;
    }


    public async Task<bool> UpdateHalfYearPerformanceAsync()
    {
        var id = await _keyFigureInfoService.GetHalfYearPerformanceIdAsync();

        KeyFigureValue MapToKeyFigureValue(PortfolioPerformance p)
        {
            return new KeyFigureValue
            {
                PortfolioId = p.PortfolioId,
                KeyFigureId = id,
                Value = p.Value
            };
        }

        var performances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();
        var halfYearPerformances = performances
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(halfYearPerformances);
        return true;
    }

}