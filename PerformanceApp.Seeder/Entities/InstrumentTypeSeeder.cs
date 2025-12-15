using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Seeder.Entities;

public class InstrumentTypeSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentTypeRepository _instrumentTypeRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var instrumentTypes = await _instrumentTypeRepository.GetInstrumentTypesAsync();

        return instrumentTypes.Any();
    }

    private static string? GetInstrumentTypeName(Staging staging) => staging.InstrumentType;
    private static InstrumentType MapToInstrumentType(string name)
    {
        return new InstrumentType { Name = name };
    }
    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var stagings = await _stagingRepository.GetStagingsAsync();
        var instrumentTypes = stagings
            .Select(GetInstrumentTypeName)
            .OfType<string>()
            .Distinct()
            .Select(MapToInstrumentType)
            .ToList();

        await _instrumentTypeRepository.AddInstrumentTypesAsync(instrumentTypes);
    }
}