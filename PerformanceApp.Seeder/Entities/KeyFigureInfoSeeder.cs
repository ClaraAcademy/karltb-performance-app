using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Seeder.Services;

namespace PerformanceApp.Seeder.Entities;

public class KeyFigureInfoSeeder(PadbContext context)
{
    private readonly IKeyFigureInfoRepository _keyFigureInfoRepository = new KeyFigureInfoRepository(context);
    private readonly IKeyFigureInfoService _keyFigureInfoService = new KeyFigureInfoService(context);

    private async Task<bool> IsPopulated()
    {
        var keyFigureInfos = await _keyFigureInfoRepository.GetKeyFigureInfosAsync();

        return keyFigureInfos.Any();
    }

    private static KeyFigureInfo MapToKeyFigureInfo(string s)
    {
        return new KeyFigureInfo { Name = s };
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var raw = KeyFigureData.GetKeyFigures();

        var keyFigureInfos = raw.Select(MapToKeyFigureInfo).ToList();

        await _keyFigureInfoRepository.AddKeyFigureInfosAsync(keyFigureInfos);
    }
}