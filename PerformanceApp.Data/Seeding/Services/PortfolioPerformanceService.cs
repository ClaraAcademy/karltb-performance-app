using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPortfolioPerformanceService
{
    Task<bool> UpdatePortfolioDayPerformancesAsync(DateOnly bankday);
    Task<bool> UpdatePortfolioMonthPerformancesAsync(DateOnly bankday);
    Task<bool> UpdatePortfolioHalfYearPerformancesAsync(DateOnly bankday);
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
    private static PortfolioPerformance MapToPortfolioPerformance(int portfolioId, DateOnly periodStart, DateOnly periodEnd, decimal value, int typeId)
    {
        return new PortfolioPerformance
        {
            PortfolioId = portfolioId,
            TypeId = typeId,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            Value = value
        };
    }
    private static PortfolioPerformance MapToPortfolioPerformance(Portfolio portfolio, DateOnly periodStart, DateOnly periodEnd, decimal value, int typeId)
    {
        return MapToPortfolioPerformance(portfolio.Id, periodStart, periodEnd, value, typeId);
    }
    private static List<PortfolioValue> GetPortfolioValuesByBankday(List<Portfolio> portfolios, DateOnly bankday)
    {
        return portfolios
            .SelectMany(p => p.PortfolioValuesNavigation)
            .Where(pv => pv.Bankday == bankday)
            .ToList();
    }
    private static int GetKey(PortfolioValue pv) => pv.PortfolioId;

    private static PortfolioPerformance MapToPortfolioPerformance(PortfolioValue current, PortfolioValue previous, int performanceTypeId, decimal perfomanceValue)
    {
        return MapToPortfolioPerformance(current.PortfolioId, current.Bankday, current.Bankday, perfomanceValue, performanceTypeId);
    }
    private PortfolioPerformance MapToPortfolioDayPerformance(PortfolioValue current, PortfolioValue previous)
    {
        var id = _performanceService.GetDayPerformanceIdAsync().Result;
        var performanceValue = _performanceService.GetPerformanceValue(current.Value!.Value, previous.Value!.Value);
        return MapToPortfolioPerformance(current, previous, id, performanceValue);
    }

    private async Task<List<PortfolioPerformance>> GetProperPortfolioDayPerformances(List<Portfolio> portfolios, DateOnly bankday)
    {
        var previousBankday = await _dateInfoService.GetPreviousBankdayAsync(bankday);

        var currValues = GetPortfolioValuesByBankday(portfolios, bankday);
        var prevValues = GetPortfolioValuesByBankday(portfolios, previousBankday);

        var performances = currValues
            .Join(prevValues, GetKey, GetKey, MapToPortfolioDayPerformance)
            .ToList();

        return performances;
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

    private static bool IsSameYear(DateOnly d1, DateOnly d2) => d1.Year == d2.Year;
    private static bool IsSameMonth(DateOnly d1, DateOnly d2) => d1.Month == d2.Month;
    private static bool IsSameYearAndMonth(DateOnly d1, DateOnly d2) => IsSameYear(d1, d2) && IsSameMonth(d1, d2);

    private static bool IsSamePerformanceType(PortfolioPerformance pp, int typeId) => pp.TypeId == typeId;

    public async Task<bool> UpdatePortfolioMonthPerformancesAsync(DateOnly bankday)
    {
        var performances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();

        var dayPerformanceId = await _performanceService.GetDayPerformanceIdAsync();
        var monthPerformanceId = await _performanceService.GetMonthPerformanceIdAsync();

        var dayPerformancesInRange = performances
            .Where(pp => IsSamePerformanceType(pp, dayPerformanceId))
            .Where(pp => IsSameYearAndMonth(pp.PeriodEnd, bankday))
            .ToList();

        var monthPerformances = dayPerformancesInRange
            .GroupBy(dp => dp.PortfolioId)
            .Select(
                g => MapToPortfolioPerformance(
                    g.First().PortfolioNavigation!,
                    g.Min(pp => pp.PeriodStart),
                    g.Max(pp => pp.PeriodEnd),
                    g.Aggregate(1m, (acc, pp) => acc * (1 + pp.Value)) - 1, // Product(1 + daily_return) - 1
                    monthPerformanceId
                )
            )
            .ToList();

        await _portfolioPerformanceRepository.AddPortfolioPerformancesAsync(monthPerformances);

        return true;
    }

    private static List<PortfolioPerformance> AggregateDayPerformances(List<PortfolioPerformance> performances, int performanceTypeId)
    {
        var aggregated = performances
            .GroupBy(pp => pp.PortfolioId)
            .Select(g => MapToPortfolioPerformance(
                g.First().PortfolioNavigation!.Id,
                g.Min(pp => pp.PeriodStart),
                g.Max(pp => pp.PeriodEnd),
                g.Aggregate(1m, (acc, pp) => acc * (1 + pp.Value)) - 1, // Product(1 + daily_return) - 1
                performanceTypeId
            ))
            .ToList();

        return aggregated;
    }
    private (DateOnly Start, DateOnly End) GetHalfYearBound(DateOnly bankday)
    {
        int half = (bankday.Month - 1) / 6;

        var startMonth = half * 6 + 1;
        var endMonth = startMonth + 5;

        var startDay = 1;
        var endDay = DateTime.DaysInMonth(bankday.Year, endMonth);

        var year = bankday.Year;

        var start = new DateOnly(year, startMonth, startDay);
        var end = new DateOnly(year, endMonth, endDay);

        return (start, end);
    }
    public async Task<bool> UpdatePortfolioHalfYearPerformancesAsync(DateOnly bankday)
    {
        var performances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();

        var dayPerformanceId = await _performanceService.GetDayPerformanceIdAsync();
        var halfYearPerformanceId = await _performanceService.GetHalfYearPerformanceIdAsync();

        var (halfYearStart, halfYearEnd) = GetHalfYearBound(bankday);

        var dayPerformancesInRange = performances
            .Where(pp => IsSamePerformanceType(pp, dayPerformanceId))
            .Where(pp => pp.PeriodEnd >= halfYearStart && pp.PeriodEnd <= halfYearEnd)
            .ToList();

        var halfYearPerformances = AggregateDayPerformances(dayPerformancesInRange, halfYearPerformanceId);

        await _portfolioPerformanceRepository.AddPortfolioPerformancesAsync(halfYearPerformances);
        return true;
    }

}