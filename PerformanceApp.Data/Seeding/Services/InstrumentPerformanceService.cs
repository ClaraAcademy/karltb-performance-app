using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IInstrumentPerformanceService
{
    Task<bool> UpdateInstrumentDayPerformancesAsync(DateOnly bankday);
}

public class InstrumentPerformanceService : IInstrumentPerformanceService
{
    private readonly IInstrumentPerformanceRepository _instrumentPerformanceRepository;
    private readonly IInstrumentPriceRepository _instrumentPriceRepository;
    private readonly IPerformanceService _performanceService;
    private readonly IDateInfoService _dateInfoService;

    public InstrumentPerformanceService(
        IInstrumentPerformanceRepository instrumentPerformanceRepository,
        IInstrumentPriceRepository instrumentPriceRepository,
        IPerformanceService performanceService,
        IDateInfoService dateInfoService
    )
    {
        _instrumentPerformanceRepository = instrumentPerformanceRepository;
        _instrumentPriceRepository = instrumentPriceRepository;
        _performanceService = performanceService;
        _dateInfoService = dateInfoService;
    }
    private static int GetKey(InstrumentPrice ip) => ip.InstrumentId;
    private static decimal GetPerformance(decimal current, decimal previous)
    {
        if (previous == 0)
        {
            return 0;
        }
        return (current - previous) / previous;
    }
    private static InstrumentPerformance MapToInstrumentPerformance(InstrumentPrice c, InstrumentPrice p, int performanceTypeId)
    {
        return new InstrumentPerformance
        {
            InstrumentId = c.InstrumentId,
            TypeId = performanceTypeId,
            PeriodStart = c.Bankday,
            PeriodEnd = c.Bankday,
            Value = GetPerformance(c.Price, p.Price)
        };
    }
    private InstrumentPerformance MapToInstrumentDayPerformance(InstrumentPrice c, InstrumentPrice p)
    {
        var id = _performanceService.GetDayPerformanceIdAsync().Result;
        return MapToInstrumentPerformance(c, p, id);
    }

    public async Task<bool> UpdateInstrumentDayPerformancesAsync(DateOnly bankday)
    {
        var bankdayIsValid = await _dateInfoService.BankdayExistsAsync(bankday);
        if (!bankdayIsValid)
        {
            return false;
        }
        var previousBankday = await _dateInfoService.GetPreviousBankdayAsync(bankday);

        var instrumentPrices = await _instrumentPriceRepository.GetInstrumentPricesAsync();
        var currentPrices = instrumentPrices
            .Where(ip => ip.Bankday == bankday);
        var previousPrices = instrumentPrices
            .Where(ip => ip.Bankday == previousBankday);

        var performanceTypeId = await _performanceService.GetDayPerformanceIdAsync();

        var dayPerformances = currentPrices
            .Join(previousPrices, GetKey, GetKey, MapToInstrumentDayPerformance)
            .ToList();

        await _instrumentPerformanceRepository.AddInstrumentPerformancesAsync(dayPerformances);
        return true;
    }
}