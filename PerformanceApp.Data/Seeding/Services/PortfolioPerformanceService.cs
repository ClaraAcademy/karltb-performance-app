using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPortfolioPerformanceService
{
    Task<bool> UpdatePortfolioDayPerformancesAsync(DateOnly bankday);
}

public class PortfolioPerformanceService : IPortfolioPerformanceService
{
    private readonly IPortfolioPerformanceRepository _portfolioPerformanceRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    private readonly IPortfolioValueRepository _portfolioValueRepository;
    private readonly IPerformanceService _performanceService;
    private readonly IDateInfoService _dateInfoService;
    public PortfolioPerformanceService(
        IPortfolioPerformanceRepository portfolioPerformanceRepository,
        IPortfolioRepository portfolioRepository,
        IPortfolioValueRepository portfolioValueRepository,
        IPerformanceService performanceService,
        IDateInfoService dateInfoService
    )
    {
        _portfolioPerformanceRepository = portfolioPerformanceRepository;
        _portfolioRepository = portfolioRepository;
        _portfolioValueRepository = portfolioValueRepository;
        _performanceService = performanceService;
        _dateInfoService = dateInfoService;
    }
    private static PortfolioPerformance MapToPortfolioPerformance(Portfolio portfolio, DateOnly periodStart, DateOnly periodEnd, decimal value, int typeId)
    {
        return new PortfolioPerformance
        {
            PortfolioId = portfolio.Id,
            TypeId = typeId,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            Value = value
        };
    }
    private async Task<List<PortfolioPerformance>> GetProperPortfolioDayPerformances(List<Portfolio> portfolios, DateOnly bankday)
    {
        var previousBankday = await _dateInfoService.GetPreviousBankdayAsync(bankday);

        var portfolioValues = portfolios
            .SelectMany(p => p.PortfolioValuesNavigation)
            .ToList();

        var currentValues = portfolioValues
            .Where(pv => pv.Bankday == bankday)
            .ToDictionary(pv => pv.PortfolioId, pv => pv.Value);

        var previousValues = portfolioValues
            .Where(pv => pv.Bankday == previousBankday)
            .ToDictionary(pv => pv.PortfolioId, pv => pv.Value);

        var dayPerformanceId = await _performanceService.GetDayPerformanceIdAsync();

        var portfolioPerformances = new List<PortfolioPerformance>();
        foreach (var portfolio in portfolios)
        {
            if (
                currentValues.TryGetValue(portfolio.Id, out var currentValue)
                && previousValues.TryGetValue(portfolio.Id, out var previousValue)
            )
            {
                var performanceValue = _performanceService.GetPerformanceValue(currentValue!.Value, previousValue!.Value);
                var portfolioPerformance = MapToPortfolioPerformance(portfolio, bankday, bankday, performanceValue, dayPerformanceId);
                portfolioPerformances.Add(portfolioPerformance);
            }
        }
        return portfolioPerformances;
    }

    private static int GetKey(Position p) => p.PortfolioId!.Value;
    private static decimal GetValue(Position position, DateOnly bankday)
    {
        var proportion = position.Proportion!;
        var price = position
            .InstrumentNavigation!
            .InstrumentPricesNavigation
            .FirstOrDefault(ip => ip.Bankday == bankday)?.Price ?? 0;

        return proportion.Value * price;
    }
    private static PortfolioPerformance MapToPortfolioPerformance(IGrouping<int, decimal> group, DateOnly bankday, int typeId)
    {
        return new PortfolioPerformance
        {
            PortfolioId = group.Key,
            PeriodStart = bankday,
            PeriodEnd = bankday,
            Value = group.Sum(),
            TypeId = typeId
        };
    }

    private async Task<List<PortfolioPerformance>> GetBenchmarkPortfolioDayPerformances(List<Portfolio> benchmarkPortfolios, DateOnly bankday)
    {
        var typeId = await _performanceService.GetDayPerformanceIdAsync();

        var performances = benchmarkPortfolios
            .SelectMany(p => p.PositionsNavigation)
            .Where(pos => pos.Bankday == bankday)
            .GroupBy(GetKey, pos => GetValue(pos, bankday))
            .Select(g => MapToPortfolioPerformance(g, bankday, typeId))
            .ToList();

        return performances;
    }

    public async Task<bool> UpdatePortfolioDayPerformancesAsync(DateOnly bankday)
    {
        var bankdayExists = await _dateInfoService.BankdayExistsAsync(bankday);
        if (!bankdayExists)
        {
            return false;
        }
        var properPortfolios = await _portfolioRepository.GetProperPortfoliosAsync();

        var portfolios = await _portfolioRepository.GetPortfoliosAsync();
        var benchmarkPortfolios = portfolios.Where(p => !properPortfolios.Any(pp => pp.Id == p.Id)).ToList();

        var portfolioPerformances = await GetProperPortfolioDayPerformances(properPortfolios.ToList(), bankday);
        var benchmarkPerformancs = await GetBenchmarkPortfolioDayPerformances(benchmarkPortfolios, bankday);

        var allPerformances = portfolioPerformances.Concat(benchmarkPerformancs).ToList();

        await _portfolioPerformanceRepository.AddPortfolioPerformancesAsync(allPerformances);
        return true;
    }
}