using System.Threading.Tasks;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;


namespace PerformanceApp.Data.Seeding;

public class DateInfoSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly DateInfoRepository _dateInfoRepository = new(context);

    private DateInfo MapToDateInfo(DateOnly bankday) => new DateInfo { Bankday = bankday };
    private DateOnly? GetBankday(Staging staging) => staging.Bankday;

    public async Task Seed()
    {
        var stagings = await _stagingRepository.GetStagingsAsync();
        var bankdays = stagings.Select(GetBankday).OfType<DateOnly>();
        var dateInfos = bankdays.Select(MapToDateInfo).ToList();

        await _dateInfoRepository.AddDateInfosAsync(dateInfos);
    }
}