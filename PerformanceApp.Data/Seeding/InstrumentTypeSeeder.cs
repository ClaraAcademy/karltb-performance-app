using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class InstrumentTypeSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentTypeRepository _instrumentTypeRepository = new(context);

    InstrumentType MapToInstrumentType(string name) => new InstrumentType { InstrumentTypeName = name };
    public void Seed()
    {
        var instrumentTypes = _stagingRepository.GetStagings()
            .Select(s => s.InstrumentType)
            .OfType<string>()
            .Select(MapToInstrumentType)
            .ToList();

        _instrumentTypeRepository.AddInstrumentTypes(instrumentTypes);

        _context.SaveChanges();
    }
}