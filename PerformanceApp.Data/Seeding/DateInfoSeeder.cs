using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;


namespace PerformanceApp.Data.Seeding;

public class DateInfoSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly DateInfoRepository _dateInfoRepository = new(context);

    DateInfo MapToDateInfo(DateOnly bankday) => new DateInfo { Bankday = bankday };

    public void Seed()
    {
        var dateInfos = _stagingRepository.GetStagings()
            .Select(s => s.Bankday)
            .OfType<DateOnly>()
            .Select(MapToDateInfo)
            .ToList();

        _dateInfoRepository.AddDateInfos(dateInfos);

        _context.SaveChanges();
    }
}