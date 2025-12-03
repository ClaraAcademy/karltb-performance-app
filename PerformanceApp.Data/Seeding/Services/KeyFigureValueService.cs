using DocumentFormat.OpenXml.Office2010.ExcelAc;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IKeyFigureValueService
{
    Task<bool> UpdateStandardDeviationAsync();
}

public class KeyFigureValueService : IKeyFigureValueService
{
    private readonly IKeyFigureValueRepository _keyFigureValueRepository;
    private readonly IKeyFigureInfoService _keyFigureInfoService;
    private readonly IPerformanceService _performanceService;
    private readonly IPortfolioPerformanceService _portfolioPerformanceService;
    private readonly IDateInfoService _dateInfoService;
    private readonly decimal AnnualizationFactor;

    public KeyFigureValueService(
        IKeyFigureValueRepository keyFigureValueRepository,
        IKeyFigureInfoService keyFigureInfoService,
        IPerformanceService performanceService,
        IPortfolioPerformanceService portfolioPerformanceService,
        IDateInfoService dateInfoService
    )
    {
        _keyFigureValueRepository = keyFigureValueRepository;
        _keyFigureInfoService = keyFigureInfoService;
        _performanceService = performanceService;
        _portfolioPerformanceService = portfolioPerformanceService;
        _dateInfoService = dateInfoService;
        AnnualizationFactor = _dateInfoService.GetAnnualizationFactorAsync().GetAwaiter().GetResult();
    }

    private record Dto(int PortfolioId, int KeyFigureId, decimal Value);
    private static Dto MapToDto(IGrouping<int, PortfolioPerformance> group, int keyFigureId)
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
            .Select(g => MapToDto(g, id))
            .Select(MapToKeyFigureValue)
            .ToList();

        await _keyFigureValueRepository.AddKeyFigureValuesAsync(keyFigureValues);
        return true;
    }
}