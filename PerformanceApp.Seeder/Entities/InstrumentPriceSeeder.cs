using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Seeder.Entities;

public class InstrumentPriceSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);
    private readonly InstrumentPriceRepository _instrumentPriceRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var instrumentPrices = await _instrumentPriceRepository.GetInstrumentPricesAsync();

        return instrumentPrices.Any();
    }
    private static bool HasPrice(Staging staging) => staging.Price.HasValue;

    private static InstrumentPrice MapToInstrumentPrice(Staging staging, Instrument instrument)
    {
        var instrumentId = instrument.Id;
        var price = staging.Price!.Value;
        var bankday = staging.Bankday!.Value;
        return new InstrumentPrice { InstrumentId = instrumentId, Price = price, Bankday = bankday };
    }

    private static string GetInstrumentName(Staging staging) => staging.InstrumentName!;
    private static string GetInstrumentName(Instrument instrument) => instrument.Name!;

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

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