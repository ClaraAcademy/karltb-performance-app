using Microsoft.EntityFrameworkCore.ChangeTracking;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IKeyFigureInfoRepository
{
    EntityEntry<KeyFigureInfo>? AddKeyFigureInfo(KeyFigureInfo keyFigureInfo);
    List<EntityEntry<KeyFigureInfo>?> AddKeyFigureInfos(List<KeyFigureInfo> keyFigureInfo);
}

public class KeyFigureInfoRepository(PadbContext context) : IKeyFigureInfoRepository
{
    private readonly PadbContext _context = context;
    private bool Equal(KeyFigureInfo lhs, KeyFigureInfo rhs)
    {
        return lhs.KeyFigureName.Equals(rhs.KeyFigureName);
    }
    private bool Exists(KeyFigureInfo keyFigureInfo)
    {
        return _context.KeyFigureInfos.AsEnumerable().Any(kfi => Equal(kfi, keyFigureInfo));
    }

    public EntityEntry<KeyFigureInfo>? AddKeyFigureInfo(KeyFigureInfo keyFigureInfo)
    {
        if (Exists(keyFigureInfo))
        {
            return null;
        }
        return _context.KeyFigureInfos.Add(keyFigureInfo);
    }
    public List<EntityEntry<KeyFigureInfo>?> AddKeyFigureInfos(List<KeyFigureInfo> keyFigureInfos)
    {
        return keyFigureInfos.Select(AddKeyFigureInfo).ToList();
    }
}