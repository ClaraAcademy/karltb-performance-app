using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentTypeRepository
{
    void AddInstrumentType(InstrumentType instrumentType);
    Task AddInstrumentTypeAsync(InstrumentType instrumentType);
    void AddInstrumentTypes(List<InstrumentType> instrumentTypes);
    Task AddInstrumentTypesAsync(List<InstrumentType> instrumentTypes);
    InstrumentType GetInstrumentType(string name);
    Task<InstrumentType> GetInstrumentTypeAsync(string name);
    List<InstrumentType> GetInstrumentTypes(List<string> names);
    IEnumerable<InstrumentType> GetInstrumentTypes();
    Task<List<InstrumentType>> GetInstrumentTypesAsync(List<string> names);
    Task<IEnumerable<InstrumentType>> GetInstrumentTypesAsync();

}

public class InstrumentTypeRepository(PadbContext context) : IInstrumentTypeRepository
{
    private readonly PadbContext _context = context;


    public void AddInstrumentType(InstrumentType instrumentType)
    {
        _context.InstrumentTypes.Add(instrumentType);
        _context.SaveChanges();
    }

    public async Task AddInstrumentTypeAsync(InstrumentType instrumentType)
    {
        await _context.InstrumentTypes.AddAsync(instrumentType);
        await _context.SaveChangesAsync();
    }

    public void AddInstrumentTypes(List<InstrumentType> instrumentTypes)
    {
        _context.InstrumentTypes.AddRange(instrumentTypes);
        _context.SaveChanges();
    }

    public async Task AddInstrumentTypesAsync(List<InstrumentType> instrumentTypes)
    {
        await _context.InstrumentTypes.AddRangeAsync(instrumentTypes);
        await _context.SaveChangesAsync();
    }

    public InstrumentType GetInstrumentType(string name)
    {
        return _context.InstrumentTypes.Single(it => it.Name == name);
    }
    public async Task<InstrumentType> GetInstrumentTypeAsync(string name)
    {
        return await _context.InstrumentTypes.SingleAsync(it => it.Name == name);
    }
    public List<InstrumentType> GetInstrumentTypes(List<string> names)
    {
        return _context.InstrumentTypes.Where(it => names.Contains(it.Name)).ToList();
    }
    public async Task<List<InstrumentType>> GetInstrumentTypesAsync(List<string> names)
    {
        return await _context.InstrumentTypes.Where(it => names.Contains(it.Name)).ToListAsync();
    }
    public IEnumerable<InstrumentType> GetInstrumentTypes()
    {
        return _context.InstrumentTypes.ToList();
    }
    public async Task<IEnumerable<InstrumentType>> GetInstrumentTypesAsync()
    {
        return await _context.InstrumentTypes.ToListAsync();
    }
}