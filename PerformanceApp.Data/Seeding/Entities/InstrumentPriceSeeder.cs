using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Seeding.Entities;

public class InstrumentPriceSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);
    private readonly InstrumentPriceRepository _instrumentPriceRepository = new(context);

    private static bool HasPrice(Staging staging) => staging.Price.HasValue;

    private static InstrumentPrice MapToInstrumentPrice(Staging staging, Instrument instrument)
    {
        var instrumentId = instrument.InstrumentId;
        var price = staging.Price!.Value;
        var bankday = staging.Bankday!.Value;
        return new InstrumentPrice { InstrumentId = instrumentId, Price = price, Bankday = bankday };
    }

    private static string GetInstrumentName(Staging staging) => staging.InstrumentName!;
    private static string GetInstrumentName(Instrument instrument) => instrument.InstrumentName!;

    public async Task Seed()
    {
        var instruments = await _instrumentRepository.GetInstrumentsAsync();
        var stagings = await _stagingRepository.GetStagingsAsync();

        var instrumentPrices = stagings
            .Where(HasPrice)
            .Join(instruments, GetInstrumentName, GetInstrumentName, MapToInstrumentPrice)
            .Distinct()
            .ToList();

        await _instrumentPriceRepository.AddInstrumentPricesAsync(instrumentPrices);
    }
}