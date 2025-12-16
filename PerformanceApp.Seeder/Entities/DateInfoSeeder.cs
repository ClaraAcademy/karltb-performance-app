using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Data.Mappers;


namespace PerformanceApp.Seeder.Entities;

public class DateInfoSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly DateInfoRepository _dateInfoRepository = new(context);

    private async Task<bool> IsPopulated()
    {
        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();

        return dateInfos.Any();
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();
        if (exists)
        {
            return;
        }

        var stagings = await _stagingRepository.GetStagingsAsync();

        var bankdays = stagings
            .Select(StagingHelper.GetBankday)
            .OfType<DateOnly>()
            .Distinct();
        var dateInfos = bankdays
            .Select(DateInfoMapper.Map)
            .ToList();

        await _dateInfoRepository.AddDateInfosAsync(dateInfos);
    }
}