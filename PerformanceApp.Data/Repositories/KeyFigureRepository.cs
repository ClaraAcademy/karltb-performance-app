using System.Runtime.CompilerServices;
using DocumentFormat.OpenXml.Office2013.PowerPoint.Roaming;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IKeyFigureInfoRepository
{
    void AddKeyFigureInfo(KeyFigureInfo keyFigureInfo);
    Task AddKeyFigureInfoAsync(KeyFigureInfo keyFigureInfo);
    void AddKeyFigureInfos(List<KeyFigureInfo> keyFigureInfos);
    Task AddKeyFigureInfosAsync(List<KeyFigureInfo> keyFigureInfos);
    IEnumerable<KeyFigureInfo> GetKeyFigureInfos();
    Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync();
}

public class KeyFigureInfoRepository(PadbContext context) : IKeyFigureInfoRepository
{
    private readonly PadbContext _context = context;

    public void AddKeyFigureInfo(KeyFigureInfo keyFigureInfo)
    {
        _context.KeyFigureInfos.Add(keyFigureInfo);
        _context.SaveChanges();
    }

    public async Task AddKeyFigureInfoAsync(KeyFigureInfo keyFigureInfo)
    {
        await _context.KeyFigureInfos.AddAsync(keyFigureInfo);
        await _context.SaveChangesAsync();
    }

    public void AddKeyFigureInfos(List<KeyFigureInfo> keyFigureInfos)
    {
        _context.AddRange(keyFigureInfos);
        _context.SaveChanges();
    }

    public async Task AddKeyFigureInfosAsync(List<KeyFigureInfo> keyFigureInfos)
    {
        await _context.AddRangeAsync(keyFigureInfos);
        await _context.SaveChangesAsync();
    }
    public IEnumerable<KeyFigureInfo> GetKeyFigureInfos()
    {
        return _context.KeyFigureInfos.ToList();
    }
    public async Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync()
    {
        return await _context.KeyFigureInfos.ToListAsync();
    }
}