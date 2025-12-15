using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IPositionValueRepository
{
    Task<List<PositionValue>> GetPositionValuesAsync();
    Task AddPositionValuesAsync(IEnumerable<PositionValue> positionValues);
}

public class PositionValueRepository(PadbContext context) : IPositionValueRepository
{
    private readonly PadbContext _context = context;

    public async Task<List<PositionValue>> GetPositionValuesAsync()
    {
        return await _context.PositionValues.ToListAsync();
    }

    public async Task AddPositionValuesAsync(IEnumerable<PositionValue> positionValues)
    {
        await _context.PositionValues.AddRangeAsync(positionValues);
        await _context.SaveChangesAsync();
    }
}