using PerformanceApp.Data.Context;
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

    public InstrumentPerformanceService(PadbContext context)
    {
        _instrumentPerformanceRepository = new InstrumentPerformanceRepository(context);
        _instrumentPriceRepository = new InstrumentPriceRepository(context);
        _performanceService = new PerformanceService(context);
        _dateInfoService = new DateInfoService(context);
    }
    private static int GetKey(InstrumentPrice ip) => ip.InstrumentId;
    private InstrumentPerformance MapToInstrumentPerformance(InstrumentPrice c, InstrumentPrice p, int performanceTypeId)
    {
        var value = _performanceService.GetPerformanceValue(c.Price, p.Price);
        return new InstrumentPerformance
        {
            InstrumentId = c.InstrumentId,
            TypeId = performanceTypeId,
            PeriodStart = c.Bankday,
            PeriodEnd = c.Bankday,
            Value = value
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

        static List<InstrumentPrice> Filter(IEnumerable<InstrumentPrice> ips, DateOnly date)
        {
            return ips.Where(ip => ip.Bankday == date).ToList();
        }
        var instrumentPrices = await _instrumentPriceRepository.GetInstrumentPricesAsync();
        var currPrices = Filter(instrumentPrices, bankday);
        var prevPrices = Filter(instrumentPrices, previousBankday);

        var performanceTypeId = await _performanceService.GetDayPerformanceIdAsync();

        var dayPerformances = currPrices
            .Join(prevPrices, GetKey, GetKey, MapToInstrumentDayPerformance)
            .ToList();

        await _instrumentPerformanceRepository.AddInstrumentPerformancesAsync(dayPerformances);
        return true;
    }
}