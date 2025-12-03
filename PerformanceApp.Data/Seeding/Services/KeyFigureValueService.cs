using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IKeyFigureValueService
{
    Task<bool> UpdateStandardDeviationAsync();
    Task<bool> UpdateTrackingErrorAsync();
}

public class KeyFigureValueService : IKeyFigureValueService
{
    private readonly IKeyFigureValueRepository _keyFigureValueRepository;
    private readonly IKeyFigureInfoService _keyFigureInfoService;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IPortfolioPerformanceService _portfolioPerformanceService;
    private readonly IPerformanceService _performanceService;
    private readonly IDateInfoService _dateInfoService;
    private readonly decimal AnnualizationFactor;
    private readonly int DayPerformanceId;

    public KeyFigureValueService(
        IKeyFigureValueRepository keyFigureValueRepository,
        IKeyFigureInfoService keyFigureInfoService,
        IPortfolioRepository portfolioRepository,
        IPortfolioPerformanceService portfolioPerformanceService,
        IPerformanceService performanceService,
        IDateInfoService dateInfoService
    )
    {
        _keyFigureValueRepository = keyFigureValueRepository;
        _keyFigureInfoService = keyFigureInfoService;
        _portfolioRepository = portfolioRepository;
        _portfolioPerformanceService = portfolioPerformanceService;
        _performanceService = performanceService;
        _dateInfoService = dateInfoService;
        AnnualizationFactor = _dateInfoService.GetAnnualizationFactorAsync().GetAwaiter().GetResult();
        DayPerformanceId = _performanceService.GetDayPerformanceIdAsync().GetAwaiter().GetResult();
    }

    private record Dto(int PortfolioId, int KeyFigureId, decimal Value);
    private static Dto Aggregate(IGrouping<int, PortfolioPerformance> group, int keyFigureId)
    {
        return new Dto
        (
            group.Key,
            keyFigureId,
            ComputeStandardDeviation(
                group.Select(pp => pp.Value)
            )
        );
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
            .Select(g => Aggregate(g, id))
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
            => new(pp.PortfolioId, -1, pp.Value - bp.Value);

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
}