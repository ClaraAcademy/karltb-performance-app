using DocumentFormat.OpenXml.Drawing.Charts;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPortfolioPerformanceService
{
    Task<bool> UpdatePortfolioDayPerformancesAsync(DateOnly bankday);
    Task<bool> UpdatePortfolioMonthPerformancesAsync(DateOnly bankday);
    Task<bool> UpdatePortfolioHalfYearPerformancesAsync(DateOnly bankday);
    Task<bool> UpdatePortfolioCumulativeDayPerformancesAsync(DateOnly bankday);

    Task<List<PortfolioPerformance>> GetPortfolioDayPerformancesAsync();
}

public class PortfolioPerformanceService(PadbContext context) : IPortfolioPerformanceService
{
    private readonly IPortfolioPerformanceRepository _portfolioPerformanceRepository = new PortfolioPerformanceRepository(context);
    private readonly IPortfolioRepository _portfolioRepository = new PortfolioRepository(context);
    private readonly IPerformanceService _performanceService = new PerformanceService(context);
    private readonly IDateInfoService _dateInfoService = new DateInfoService(context);

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
    private static decimal GetValue(Position position, DateOnly bankday, int typeId)
    {
        var proportion = position.Proportion!;
        var price = position
            .InstrumentNavigation!
            // .InstrumentPricesNavigation
            .InstrumentPerformancesNavigation
            .FirstOrDefault(
                ip => ip.PeriodStart == bankday && ip.PeriodEnd == bankday && ip.TypeId == typeId
            )?.Value ?? 0;


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
            .GroupBy(GetKey, pos => GetValue(pos, bankday, typeId))
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

    private static List<PortfolioPerformance> AggregateDayPerformances(List<PortfolioPerformance> performances, int performanceTypeId)
    {
        var aggregated = performances
            .GroupBy(pp => pp.PortfolioId)
            .Select(g => MapToPortfolioPerformance(
                g.First().PortfolioNavigation!.Id,
                g.Min(pp => pp.PeriodStart),
                g.Max(pp => pp.PeriodEnd),
                g.Aggregate(1M, (acc, pp) => acc * (1M + pp.Value)) - 1M, // Product(1 + daily_return) - 1
                performanceTypeId
            ))
            .ToList();

        return aggregated;
    }

    private async Task<bool> UpdatePortfolioAggregatePerformancesAsync(Func<DateOnly, DateOnly, bool> filter, int typeId)
    {
        var performances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();
        var dayPerformanceId = await _performanceService.GetDayPerformanceIdAsync();
        var dayPerformances = performances
            .Where(pp => pp.TypeId == dayPerformanceId);
        var filteredDayPerformances = dayPerformances
            .Where(pp => filter(pp.PeriodStart, pp.PeriodEnd))
            .ToList();

        var aggregatedPerformances = AggregateDayPerformances(filteredDayPerformances, typeId);

        await _portfolioPerformanceRepository.AddPortfolioPerformancesAsync(aggregatedPerformances);
        return true;
    }

    public async Task<bool> UpdatePortfolioMonthPerformancesAsync(DateOnly bankday)
    {
        var monthPerformanceId = await _performanceService.GetMonthPerformanceIdAsync();

        bool filter(DateOnly start, DateOnly end) => IsSameYearAndMonth(end, bankday);

        return await UpdatePortfolioAggregatePerformancesAsync(filter, monthPerformanceId);
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
        var halfYearPerformanceId = await _performanceService.GetHalfYearPerformanceIdAsync();

        var (halfYearStart, halfYearEnd) = GetHalfYearBound(bankday);
        bool filter(DateOnly start, DateOnly end) => start >= halfYearStart && end <= halfYearEnd;

        return await UpdatePortfolioAggregatePerformancesAsync(filter, halfYearPerformanceId);
    }

    public async Task<bool> UpdatePortfolioCumulativeDayPerformancesAsync(DateOnly bankday)
    {
        var cumulativeDayPerformanceId = await _performanceService.GetCumulativeDayPerformanceIdAsync();

        bool filter(DateOnly start, DateOnly end) => end <= bankday;

        return await UpdatePortfolioAggregatePerformancesAsync(filter, cumulativeDayPerformanceId);
    }

    public async Task<List<PortfolioPerformance>> GetPortfolioDayPerformancesAsync()
    {
        var performances = await _portfolioPerformanceRepository.GetPortfolioPerformancesAsync();
        var dayPerformanceId = await _performanceService.GetDayPerformanceIdAsync();
        var dayPerformances = performances
            .Where(pp => pp.TypeId == dayPerformanceId)
            .ToList();

        return dayPerformances;
    }

}