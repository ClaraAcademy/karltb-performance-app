using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IStagingRepository
{
    EntityEntry<Staging>? AddStaging(Staging staging);
    List<EntityEntry<Staging>?> AddStagings(List<Staging> stagings);
}

public class StagingRepository(PadbContext context) : IStagingRepository
{
    private readonly PadbContext _context = context;

    private static bool Equal(Staging lhs, Staging rhs)
    {
        return lhs.Bankday == rhs.Bankday
            && lhs.InstrumentName == rhs.InstrumentName
            && lhs.InstrumentType == rhs.InstrumentType;
    }

    private bool Exists(Staging staging) => _context.Stagings.Any(s => Equal(s, staging));

    public EntityEntry<Staging>? AddStaging(Staging staging) => Exists(staging) ? null : _context.Stagings.Add(staging);
    public List<EntityEntry<Staging>?> AddStagings(List<Staging> stagings) => stagings.Select(AddStaging).ToList();

}