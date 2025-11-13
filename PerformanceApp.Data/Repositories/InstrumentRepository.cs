using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentRepository
{
    EntityEntry<Instrument>? AddInstrument(Instrument instrument);
    List<EntityEntry<Instrument>?> AddInstruments(List<Instrument> instruments);
}

public class InstrumentRepository(PadbContext context) : IInstrumentRepository
{
    private readonly PadbContext _context = context;

    private static bool Equal(Instrument lhs, Instrument rhs)
    {
        return lhs.InstrumentId == rhs.InstrumentId
            && lhs.InstrumentName == rhs.InstrumentName
            && lhs.InstrumentTypeId == rhs.InstrumentTypeId;
    }

    private bool Exists(Instrument instrument) => _context.Instruments.Any(i => Equal(i, instrument));

    public EntityEntry<Instrument>? AddInstrument(Instrument instrument)
    {
        return Exists(instrument) ? null : _context.Instruments.Add(instrument);
    }
    public List<EntityEntry<Instrument>?> AddInstruments(List<Instrument> instruments)
    {
        return instruments.Select(AddInstrument).ToList();
    }

}