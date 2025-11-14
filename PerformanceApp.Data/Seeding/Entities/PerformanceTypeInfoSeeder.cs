using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Entities;

public class PerformanceTypeSeeder(PadbContext context)
{
    private readonly PerformanceTypeRepository _performanceTypeInfoRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var performanceTypeInfos = await _performanceTypeInfoRepository.GetPerformanceTypeInfosAsync();

        return performanceTypeInfos.Any();
    }

    private PerformanceType MapToPerformanceTypeInfo(string s)
    {
        return new PerformanceType { Name = s };
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var raw = new List<string>
        {
            "Day Performance",
            "Month Performance",
            "Half-Year Performance",
            "Cumulative Day Performance"
        };

        var performanceTypeInfos = raw.Select(MapToPerformanceTypeInfo).ToList();

        await _performanceTypeInfoRepository.AddPerformanceTypesAsync(performanceTypeInfos);
    }
}