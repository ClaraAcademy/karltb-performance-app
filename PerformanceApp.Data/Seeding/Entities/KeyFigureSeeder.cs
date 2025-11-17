using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using System.Threading.Tasks;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Data.Seeding.Queries;

namespace PerformanceApp.Data.Seeding.Entities;

public class KeyFigureSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly KeyFigureInfoRepository _keyFigureInfoRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var keyFigureInfos = await _keyFigureInfoRepository.GetKeyFigureInfosAsync();

        return keyFigureInfos.Any();
    }

    KeyFigureInfo MapToKeyFigureInfo(string name) => new KeyFigureInfo { Name = name };

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var raw = new List<string>
        {
            KeyFigureData.StandardDeviation,
            KeyFigureData.TrackingError,
            KeyFigureData.AnnualisedCumulativeReturn,
            KeyFigureData.InformationRatio,
            KeyFigureData.HalfYearPerformance,
        };

        var keyFigureInfos = raw.Select(MapToKeyFigureInfo).ToList();

        await _keyFigureInfoRepository.AddKeyFigureInfosAsync(keyFigureInfos);

        foreach (var query in KeyFigureQueries.Queries)
        {
            await SqlExecutor.ExecuteQueryAsync(_context, query);
        }
    }
}