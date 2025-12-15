using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Services;
using PerformanceApp.Seeder.Utilities;

namespace PerformanceApp.Seeder.Entities;

public class PositionSeeder(PadbContext context)
{
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly PositionRepository _positionRepository = new(context);
    private readonly IPositionService _positionService = new PositionService(context);

    private async Task<bool> IsPopulated()
    {
        var positions = await _positionRepository.GetPositionsAsync();
        return positions.Any();
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();
        var bankdays = BankdayHelper.GetOrderedBankdays(dateInfos);

        foreach (var bankday in bankdays)
        {
            await _positionService.UpdatePositionsAsync(bankday);
        }
    }

}