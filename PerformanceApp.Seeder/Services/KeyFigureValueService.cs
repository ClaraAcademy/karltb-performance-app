using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Utilities;
using PerformanceApp.Data.Constants;

namespace PerformanceApp.Seeder.Services;

public interface IKeyFigureValueService
{
    Task<bool> UpdateStandardDeviationsAsync();
    Task<bool> UpdateTrackingErrorsAsync();
    Task<bool> UpdateAnnualisedCumulativeReturnsAsync();
    Task<bool> UpdateInformationRatiosAsync();
    Task<bool> UpdateHalfYearPerformancesAsync();
}

public class KeyFigureValueService(PadbContext context) : IKeyFigureValueService
{
    private readonly IKeyFigureValueRepository _keyFigureValueRepository = new KeyFigureValueRepository(context);
    private readonly IKeyFigureInfoService _keyFigureInfoService = new KeyFigureInfoService(context);
    private readonly IPortfolioRepository _portfolioRepository = new PortfolioRepository(context);
    private readonly IPortfolioPerformanceService _portfolioPerformanceService = new PortfolioPerformanceService(context);
    private readonly IPortfolioPerformanceRepository _portfolioPerformanceRepository = new PortfolioPerformanceRepository(context);
    private readonly IPerformanceService _performanceService = new PerformanceService(context);
    private readonly IDateInfoService _dateInfoService = new DateInfoService(context);

    private record Dto(int PortfolioId, int KeyFigureId, decimal Value);
    private static Dto Aggregate(IGrouping<int, PortfolioPerformance> group, int KeyFigureId, Func<IEnumerable<decimal>, decimal> func)
    {
        var values = group.Select(pp => pp.Value);
        var aggregatedValues = func(values);
        return new Dto(group.Key, KeyFigureId, aggregatedValues);
    }
    private static Dto Aggregate(IGrouping<int, Dto> group, int KeyFigureId, Func<IEnumerable<decimal>, decimal> func)
    {
        var portfolioId = group.Key;
        var values = group.Select(dto => dto.Value);
        var aggregatedValues = func(values);
        return new Dto(portfolioId, KeyFigureId, aggregatedValues);
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

    public async Task<bool> UpdateStandardDeviationsAsync()
    {
        var id = await _keyFigureInfoService.GetStandardDeviationIdAsync();
        var dayPerformances = await _portfolioPerformanceService.GetPortfolioDayPerformancesAsync();

        decimal ComputeStandardDeviation(IEnumerable<decimal> dailyReturns)
        {
            var annualizationFactor = _dateInfoService
                .GetAnnualizationFactorAsync()
                .Result;
            var stdDev = DecimalMath.StandardDeviation(dailyReturns);
            return DecimalMath.SquareRoot(annualizationFactor) * stdDev;
        }

        var keyFigureValues = dayPerformances
            .GroupBy(pp => pp.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeStandardDeviation))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(keyFigureValues);
        return true;
    }

    private List<Dto> GetActiveReturn(Portfolio portfolio)
    {
        var benchmark = portfolio
            .PortfolioPortfolioBenchmarkEntityNavigation
            .Select(b => b.BenchmarkPortfolioNavigation)
            .FirstOrDefault();

        if (benchmark == null)
        {
            return [];
        }

        IEnumerable<PortfolioPerformance> GetPortfolioDayPerformances(Portfolio portfolio)
        {
            var DayPerformanceId = _performanceService
                .GetDayPerformanceIdAsync()
                .Result;
            return portfolio
                .PortfolioPerformancesNavigation
                .Where(pp => pp.TypeId == DayPerformanceId)
                .ToList();
        }

        var portfolioDayPerformances = GetPortfolioDayPerformances(portfolio);
        var benchmarkDayPerformances = GetPortfolioDayPerformances(benchmark);

        static (DateOnly, DateOnly) GetKey(PortfolioPerformance pp)
        {
            return (pp.PeriodStart, pp.PeriodEnd);
        }
        static Dto Map(PortfolioPerformance pp, PortfolioPerformance bp)
        {
            return new Dto(pp.PortfolioId, -1, pp.Value - bp.Value);
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

    public async Task<bool> UpdateTrackingErrorsAsync()
    {
        var id = await _keyFigureInfoService.GetTrackingErrorIdAsync();

        var actualPortfolios = await _portfolioRepository.GetProperPortfoliosAsync();
        var portfolios = actualPortfolios.ToList();

        var activeReturns = GetActiveReturns(portfolios);

        decimal ComputeTrackingError(IEnumerable<decimal> activeReturns)
        {
            var annualizationFactor = _dateInfoService
                .GetAnnualizationFactorAsync()
                .Result;
            var factor = DecimalMath.SquareRoot(annualizationFactor);
            var dev = DecimalMath.StandardDeviation(activeReturns);

            return factor * dev;
        }

        var trackingErrors = activeReturns
            .GroupBy(ar => ar.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeTrackingError))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(trackingErrors);
        return true;
    }

    public async Task<bool> UpdateAnnualisedCumulativeReturnsAsync()
    {
        var id = await _keyFigureInfoService.GetAnnualizedCumulativeReturnIdAsync();

        var dayPerformances = await _portfolioPerformanceService.GetPortfolioDayPerformancesAsync();

        decimal ComputeAnnualizedCumulativeReturn(IEnumerable<decimal> dailyReturns)
        {
            var plusOne = dailyReturns.Select(r => 1M + r);
            var product = DecimalMath.Product(plusOne);

            var annualizationFactor = _dateInfoService
                .GetAnnualizationFactorAsync()
                .Result;

            return DecimalMath.Power(product, annualizationFactor) - 1M;
        }

        var annualisedCumulativeReturns = dayPerformances
            .GroupBy(pp => pp.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeAnnualizedCumulativeReturn))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(annualisedCumulativeReturns);
        return true;
    }

    public async Task<bool> UpdateInformationRatiosAsync()
    {
        var id = await _keyFigureInfoService.GetInformationRatioIdAsync();

        var actualPortfolios = await _portfolioRepository.GetProperPortfoliosAsync();
        var portfolios = actualPortfolios.ToList();

        var activeReturns = GetActiveReturns(portfolios);

        decimal ComputeInformationRatio(IEnumerable<decimal> activeReturns)
        {
            var avg = activeReturns.Average();
            var dev = DecimalMath.StandardDeviation(activeReturns);

            var annualizationFactor = _dateInfoService
                .GetAnnualizationFactorAsync()
                .Result;

            var fraq = dev == 0M ? 0M : avg / dev;

            var factor = (decimal)Math.Sqrt((double)annualizationFactor);

            return fraq * factor;
        }

        var informationRatios = activeReturns
            .GroupBy(ar => ar.PortfolioId)
            .Select(g => Aggregate(g, id, ComputeInformationRatio))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(informationRatios);
        return true;
    }


    public async Task<bool> UpdateHalfYearPerformancesAsync()
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
            .Where(pp => pp.PerformanceTypeNavigation.Name == PerformanceTypeConstants.HalfYear)
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(halfYearPerformances);
        return true;
    }

}