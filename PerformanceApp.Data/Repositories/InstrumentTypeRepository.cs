using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IInstrumentTypeRepository
{
    EntityEntry<InstrumentType>? AddInstrumentType(InstrumentType instrumentType);
    List<EntityEntry<InstrumentType>?> AddInstrumentTypes(List<InstrumentType> instrumentTypes);

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
}