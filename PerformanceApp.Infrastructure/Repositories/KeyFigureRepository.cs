using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IKeyFigureInfoRepository
{
    Task AddKeyFigureInfosAsync(List<KeyFigureInfo> keyFigureInfos);
    Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync();
}

public class KeyFigureInfoRepository(PadbContext context) : IKeyFigureInfoRepository
{
    private readonly PadbContext _context = context;

    public async Task AddKeyFigureInfosAsync(List<KeyFigureInfo> keyFigureInfos)
    {
        await _context.AddRangeAsync(keyFigureInfos);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync()
    {
        return await _context.KeyFigureInfos.ToListAsync();
    }
}