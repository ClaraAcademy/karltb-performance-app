using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentRepository
{
    Task AddInstrumentsAsync(List<Instrument> instruments);
    Task<List<Instrument>> GetInstrumentsAsync();
}

public class InstrumentRepository(PadbContext context) : IInstrumentRepository
{
    private readonly PadbContext _context = context;

    public async Task AddInstrumentsAsync(List<Instrument> instruments)
    {
        await _context.Instruments.AddRangeAsync(instruments);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Instrument>> GetInstrumentsAsync()
    {
        return await _context.Instruments.ToListAsync();
    }
}