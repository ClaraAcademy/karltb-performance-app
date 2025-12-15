using Microsoft.EntityFrameworkCore;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IPerformanceTypeRepository
{
    Task AddPerformanceTypesAsync(List<PerformanceType> performanceTypeInfos);
    Task<List<PerformanceType>> GetPerformanceTypeInfosAsync();
}

public class PerformanceTypeRepository(PadbContext context) : IPerformanceTypeRepository
{
    private readonly PadbContext _context = context;

    public async Task AddPerformanceTypesAsync(List<PerformanceType> performanceTypeInfos)
    {
        await _context.PerformanceTypeInfos.AddRangeAsync(performanceTypeInfos);
        await _context.SaveChangesAsync();
    }
    public async Task<List<PerformanceType>> GetPerformanceTypeInfosAsync()
    {
        return await _context.PerformanceTypeInfos.ToListAsync();
    }

}