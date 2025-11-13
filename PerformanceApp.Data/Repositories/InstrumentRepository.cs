using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentRepository
{
    EntityEntry<Instrument>? AddInstrument(Instrument instrument);
    List<EntityEntry<Instrument>?> AddInstruments(List<Instrument> instruments);
    Instrument GetInstrument(string name);
    Task<Instrument> GetInstrumentAsync(string name);
    List<Instrument> GetInstruments();
    List<Instrument> GetInstruments(List<string> names);
    Task<List<Instrument>> GetInstrumentsAsync();
    Task<List<Instrument>> GetInstrumentsAsync(List<string> names);
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

    public Instrument GetInstrument(string name)
    {
        return _context.Instruments
            .Single(i => i.InstrumentName == name);
    }
    public async Task<Instrument> GetInstrumentAsync(string name)
    {
        return await _context.Instruments
            .SingleAsync(i => i.InstrumentName == name);
    }
    public List<Instrument> GetInstruments()
    {
        return _context.Instruments.ToList();
    }
    public async Task<List<Instrument>> GetInstrumentsAsync()
    {
        return await _context.Instruments.ToListAsync();
    }
    public List<Instrument> GetInstruments(List<string> names)
    {
        return _context.Instruments
            .Where(i => i.InstrumentName != null)
            .Where(i => names.Contains(i.InstrumentName))
            .ToList();
    }
    public async Task<List<Instrument>> GetInstrumentsAsync(List<string> names)
    {
        return await _context.Instruments
            .Where(i => i.InstrumentName != null)
            .Where(i => names.Contains(i.InstrumentName))
            .ToListAsync();
    }

}