using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IPerformanceTypeInfoRepository
{
    Task AddPerformanceTypeAsync(PerformanceTypeInfo performanceTypeInfo);
    Task AddPerformanceTypesAsync(List<PerformanceTypeInfo> performanceTypeInfos);
    Task<IEnumerable<PerformanceTypeInfo>> GetPerformanceTypeInfosAsync();
}

public class PerformanceTypeInfoRepository(PadbContext context) : IPerformanceTypeInfoRepository
{
    private readonly PadbContext _context = context;

    public async Task AddPerformanceTypeAsync(PerformanceTypeInfo performanceTypeInfo)
    {
        await _context.PerformanceTypeInfos.AddAsync(performanceTypeInfo);
        await _context.SaveChangesAsync();
    }
    public async Task AddPerformanceTypesAsync(List<PerformanceTypeInfo> performanceTypeInfos)
    {
        await _context.PerformanceTypeInfos.AddRangeAsync(performanceTypeInfos);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<PerformanceTypeInfo>> GetPerformanceTypeInfosAsync()
    {
        return await _context.PerformanceTypeInfos.ToListAsync();
    }

}