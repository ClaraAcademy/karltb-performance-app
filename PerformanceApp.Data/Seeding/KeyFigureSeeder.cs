
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using System.Threading.Tasks;

namespace PerformanceApp.Data.Seeding;

public class KeyFigureSeeder(PadbContext context)
{
    private readonly KeyFigureInfoRepository _keyFigureInfoRepository = new(context);

    KeyFigureInfo MapToKeyFigureInfo(string name) => new KeyFigureInfo { KeyFigureName = name };

    public async Task Seed()
    {
        var raw = new List<string>
        {
            "Standard Deviation",
            "Tracking Error",
            "Annualised Cumulative Return",
            "Information Ratio",
            "Half-Year Performance"
        };

        var keyFigureInfos = raw.Select(MapToKeyFigureInfo).ToList();

        await _keyFigureInfoRepository.AddKeyFigureInfosAsync(keyFigureInfos);
    }
}