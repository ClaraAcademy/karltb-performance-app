using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IStagingRepository
{
    void AddStaging(Staging staging);
    Task AddStagingAsync(Staging staging);
    void AddStagings(List<Staging> stagings);
    Task AddStagingsAsync(List<Staging> stagings);
    List<Staging> GetStagings();
    Task<List<Staging>> GetStagingsAsync();
}

public class StagingRepository(PadbContext context) : IStagingRepository
{
    private readonly PadbContext _context = context;


    public void AddStaging(Staging staging)
    {
        _context.Stagings.Add(staging);
        _context.SaveChanges();
    }

    public async Task AddStagingAsync(Staging staging)
    {
        await _context.Stagings.AddAsync(staging);
        await _context.SaveChangesAsync();
    }

    public void AddStagings(List<Staging> stagings)
    {
        _context.Stagings.AddRange(stagings);
        _context.SaveChanges();
    }

    public async Task AddStagingsAsync(List<Staging> stagings)
    {
        await _context.Stagings.AddRangeAsync(stagings);
        await _context.SaveChangesAsync();
    }

    public List<Staging> GetStagings()
    {
        return _context.Stagings.ToList();
    }
    public async Task<List<Staging>> GetStagingsAsync()
    {
        return await _context.Stagings.ToListAsync();
    }

}