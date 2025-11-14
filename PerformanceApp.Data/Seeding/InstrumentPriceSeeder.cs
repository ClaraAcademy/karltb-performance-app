using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Seeding;

public class InstrumentPriceSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);
    private readonly InstrumentPriceRepository _instrumentPriceRepository = new(context);

    private static bool HasPrice(Staging staging) => staging.Price.HasValue;

    private static InstrumentPrice MapToInstrumentPrice(Staging staging, Instrument instrument)
    {
        return new InstrumentPrice
        {
            InstrumentId = instrument.InstrumentId,
            Price = staging.Price!.Value
        };
    }

    public async void Seed()
    {
        var instruments = await _instrumentRepository.GetInstrumentsAsync();
        var stagings = await _stagingRepository.GetStagingsAsync();

        var instrumentPrices = stagings
            .Where(HasPrice)
            .Join(
                instruments,
                s => s.InstrumentName,
                i => i.InstrumentName,
                MapToInstrumentPrice
            ).ToList();

        await _instrumentPriceRepository.AddInstrumentPricesAsync(instrumentPrices);
    }
}