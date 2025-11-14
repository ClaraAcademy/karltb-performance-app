using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class InstrumentTypeSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentTypeRepository _instrumentTypeRepository = new(context);

    private static string? GetInstrumentTypeName(Staging staging) => staging.InstrumentType;
    private static InstrumentType MapToInstrumentType(string name)
    {
        return new InstrumentType { InstrumentTypeName = name };
    }
    public async Task Seed()
    {
        var stagings = await _stagingRepository.GetStagingsAsync();
        var instrumentTypes = stagings
            .Select(GetInstrumentTypeName)
            .OfType<string>()
            .Select(MapToInstrumentType)
            .ToList();

        await _instrumentTypeRepository.AddInstrumentTypesAsync(instrumentTypes);
    }
}