using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Services;
using PerformanceApp.Data.Seeding.Utilities;

namespace PerformanceApp.Data.Seeding.Entities;

public class PositionValueSeeder(PadbContext context)
{
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly IPositionValueService _positionValueService = new PositionValueService(context);
    private readonly IPositionValueRepository _positionValueRepository = new PositionValueRepository(context);

    private async Task<bool> IsPopulated()
    {
        var positionValues = await _positionValueRepository.GetPositionValuesAsync();
        return positionValues.Any();
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
            await _positionValueService.UpdatePositionValuesAsync(bankday);
        }
    }
}