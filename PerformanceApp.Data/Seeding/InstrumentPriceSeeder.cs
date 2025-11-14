using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Seeding;

public class InstrumentPriceSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);
    private readonly InstrumentPriceRepository _instrumentPriceRepository = new(context);

    public void Seed()
    {
        var instruments = _instrumentRepository.GetInstruments();

        var instrumentPrices = _stagingRepository.GetStagings()
            .Where(s => s.Price != null)
            .Join(
                instruments,
                s => s.InstrumentName,
                i => i.InstrumentName,
                (s, i) => new InstrumentPrice
                {
                    InstrumentId = i.InstrumentId,
                    Price = s.Price!.Value
                }
            ).ToList();

        _instrumentPriceRepository.AddInstrumentPrices(instrumentPrices);

        _context.SaveChanges();
    }
}