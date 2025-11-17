using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IPerformanceTypeRepository
{
    Task AddPerformanceTypesAsync(List<PerformanceType> performanceTypeInfos);
    Task<IEnumerable<PerformanceType>> GetPerformanceTypeInfosAsync();
}

public class PerformanceTypeRepository(PadbContext context) : IPerformanceTypeRepository
{
    private readonly PadbContext _context = context;

    public async Task AddPerformanceTypesAsync(List<PerformanceType> performanceTypeInfos)
    {
        await _context.PerformanceTypeInfos.AddRangeAsync(performanceTypeInfos);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<PerformanceType>> GetPerformanceTypeInfosAsync()
    {
        return await _context.PerformanceTypeInfos.ToListAsync();
    }

}