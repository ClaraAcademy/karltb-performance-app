using System.Threading.Tasks;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Entities;

public class InstrumentSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly InstrumentTypeRepository _instrumentTypeRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);

    private class Key
    {
        public required string InstrumentName { get; set; }
        public required string InstrumentTypeName { get; set; }
    }

    private static Key? MapToKey(Staging staging)
    {
        string? instrument = staging.InstrumentName;
        string? instrumentType = staging.InstrumentType;

        if (instrument == null || instrumentType == null)
        {
            return null;
        }

        return new Key { InstrumentName = instrument, InstrumentTypeName = instrumentType };
    }

    private static Instrument MapToInstrument(Key key, InstrumentType instrumentType)
    {
        return new Instrument
        {
            InstrumentName = key.InstrumentName,
            InstrumentTypeId = instrumentType.InstrumentTypeId
        };
    }

    public async Task Seed()
    {
        var stagings = await _stagingRepository.GetStagingsAsync();

        var instrumentTypeNames = stagings
            .Select(s => s.InstrumentType)
            .OfType<string>()
            .Distinct()
            .ToList();

        var instrumentTypes = await _instrumentTypeRepository.GetInstrumentTypesAsync(instrumentTypeNames);

        var keys = stagings.Select(MapToKey).OfType<Key>().Distinct();

        var instruments = keys
            .Join(instrumentTypes, k => k.InstrumentTypeName, it => it.InstrumentTypeName, MapToInstrument)
            .ToList();

        await _instrumentRepository.AddInstrumentsAsync(instruments);
    }
}