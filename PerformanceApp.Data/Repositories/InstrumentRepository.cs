using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentRepository
{
    void AddInstrument(Instrument instrument);
    Task AddInstrumentAsync(Instrument instrument);
    void AddInstruments(List<Instrument> instruments);
    Task AddInstrumentsAsync(List<Instrument> instruments);
    Instrument? GetInstrument(string name);
    Task<Instrument?> GetInstrumentAsync(string name);
    List<Instrument> GetInstruments();
    List<Instrument> GetInstruments(List<string> names);
    Task<List<Instrument>> GetInstrumentsAsync();
    Task<List<Instrument>> GetInstrumentsAsync(List<string> names);
}

public class InstrumentRepository(PadbContext context) : IInstrumentRepository
{
    private readonly PadbContext _context = context;

    public void AddInstrument(Instrument instrument)
    {
        _context.Instruments.Add(instrument);
        _context.SaveChanges();
    }
    public async Task AddInstrumentAsync(Instrument instrument)
    {
        await _context.Instruments.AddAsync(instrument);
        await _context.SaveChangesAsync();
    }
    public void AddInstruments(List<Instrument> instruments)
    {
        _context.Instruments.AddRange(instruments);
        _context.SaveChanges();
    }
    public async Task AddInstrumentsAsync(List<Instrument> instruments)
    {
        await _context.Instruments.AddRangeAsync(instruments);
        await _context.SaveChangesAsync();
    }

    public Instrument? GetInstrument(string name)
    {
        return _context.Instruments.SingleOrDefault(i => i.Name == name);
    }
    public async Task<Instrument?> GetInstrumentAsync(string name)
    {
        return await _context.Instruments.SingleOrDefaultAsync(i => i.Name == name);
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
            .Where(i => i.Name != null)
            .Where(i => names.Contains(i.Name!))
            .ToList();
    }
    public async Task<List<Instrument>> GetInstrumentsAsync(List<string> names)
    {
        return await _context.Instruments
            .Where(i => i.Name != null)
            .Where(i => names.Contains(i.Name!))
            .ToListAsync();
    }

}