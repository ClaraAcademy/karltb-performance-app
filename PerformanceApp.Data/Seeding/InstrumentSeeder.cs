using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class InstrumentSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentTypeRepository _instrumentTypeRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);

    public void Seed()
    {
        var stagings = _stagingRepository.GetStagings();

        var instrumentTypeNames = stagings
            .Select(s => s.InstrumentType)
            .OfType<string>()
            .Distinct()
            .ToList();

        var instrumentTypes = _instrumentTypeRepository.GetInstrumentTypes(instrumentTypeNames);
        var instruments = stagings
            .Select(s => new { s.InstrumentName, s.InstrumentType })
            .Distinct()
            .Join(
                instrumentTypes,
                s => s.InstrumentType,
                it => it.InstrumentTypeName,
                (s, it) => new Instrument
                {
                    InstrumentName = s.InstrumentName,
                    InstrumentTypeId = it.InstrumentTypeId
                }
            ).ToList();

        _instrumentRepository.AddInstruments(instruments);

        _context.SaveChanges();
    }
}