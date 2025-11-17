using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IStagingRepository
{
    Task AddStagingsAsync(List<Staging> stagings);
    Task<List<Staging>> GetStagingsAsync();
}

public class StagingRepository(PadbContext context) : IStagingRepository
{
    private readonly PadbContext _context = context;

    public async Task AddStagingsAsync(List<Staging> stagings)
    {
        await _context.Stagings.AddRangeAsync(stagings);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Staging>> GetStagingsAsync()
    {
        return await _context.Stagings.ToListAsync();
    }

}