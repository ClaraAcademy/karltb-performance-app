using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentTypeRepository
{
    Task AddInstrumentTypesAsync(List<InstrumentType> instrumentTypes);
    Task<List<InstrumentType>> GetInstrumentTypesAsync(List<string> names);
    Task<IEnumerable<InstrumentType>> GetInstrumentTypesAsync();
}

public class InstrumentTypeRepository(PadbContext context) : IInstrumentTypeRepository
{
    private readonly PadbContext _context = context;

    public async Task AddInstrumentTypesAsync(List<InstrumentType> instrumentTypes)
    {
        await _context.InstrumentTypes.AddRangeAsync(instrumentTypes);
        await _context.SaveChangesAsync();
    }
    public async Task<List<InstrumentType>> GetInstrumentTypesAsync(List<string> names)
    {
        return await _context.InstrumentTypes.Where(it => names.Contains(it.Name)).ToListAsync();
    }
        public async Task<IEnumerable<InstrumentType>> GetInstrumentTypesAsync()
    {
        return await _context.InstrumentTypes.ToListAsync();
    }
}