using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentTypeRepository
{
    EntityEntry<InstrumentType>? AddInstrumentType(InstrumentType instrumentType);
    List<EntityEntry<InstrumentType>?> AddInstrumentTypes(List<InstrumentType> instrumentTypes);
    InstrumentType GetInstrumentType(string name);
    Task<InstrumentType> GetInstrumentTypeAsync(string name);
    List<InstrumentType> GetInstrumentTypes(List<string> names);
    Task<List<InstrumentType>> GetInstrumentTypesAsync(List<string> names);

}

public class InstrumentTypeRepository(PadbContext context) : IInstrumentTypeRepository
{
    private readonly PadbContext _context = context;

    private static bool Equal(InstrumentType lhs, InstrumentType rhs) => lhs.InstrumentTypeName == rhs.InstrumentTypeName;
    private bool Exists(InstrumentType instrumentType) => _context.InstrumentTypes.Any(it => Equal(it, instrumentType));

    public EntityEntry<InstrumentType>? AddInstrumentType(InstrumentType instrumentType)
    {
        return Exists(instrumentType) ? null : _context.InstrumentTypes.Add(instrumentType);
    }
    public List<EntityEntry<InstrumentType>?> AddInstrumentTypes(List<InstrumentType> instrumentTypes)
    {
        return instrumentTypes.Select(AddInstrumentType).ToList();
    }
    public InstrumentType GetInstrumentType(string name)
    {
        return _context.InstrumentTypes.Single(it => it.InstrumentTypeName == name);
    }
    public async Task<InstrumentType> GetInstrumentTypeAsync(string name)
    {
        return await _context.InstrumentTypes.SingleAsync(it => it.InstrumentTypeName == name);
    }
    public List<InstrumentType> GetInstrumentTypes(List<string> names)
    {
        return _context.InstrumentTypes.Where(it => names.Contains(it.InstrumentTypeName)).ToList();
    }
    public async Task<List<InstrumentType>> GetInstrumentTypesAsync(List<string> names)
    {
        return await _context.InstrumentTypes.Where(it => names.Contains(it.InstrumentTypeName)).ToListAsync();
    }
}