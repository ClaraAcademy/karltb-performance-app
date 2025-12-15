using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Entities;

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

        var raw = PerformanceTypeData.PerformanceTypes;

        var performanceTypeInfos = raw.Select(MapToPerformanceTypeInfo).ToList();

        await _performanceTypeInfoRepository.AddPerformanceTypesAsync(performanceTypeInfos);
    }
}