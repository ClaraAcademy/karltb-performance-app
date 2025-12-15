using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;


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

    private DateInfo MapToDateInfo(DateOnly bankday) => new DateInfo { Bankday = bankday };
    private DateOnly? GetBankday(Staging staging) => staging.Bankday;

    public async Task Seed()
    {
        var exists = await IsPopulated();
        if (exists)
        {
            return;
        }

        var stagings = await _stagingRepository.GetStagingsAsync();
        var bankdays = stagings.Select(GetBankday)
            .OfType<DateOnly>()
            .Distinct();
        var dateInfos = bankdays.Select(MapToDateInfo).ToList();

        await _dateInfoRepository.AddDateInfosAsync(dateInfos);
    }
}