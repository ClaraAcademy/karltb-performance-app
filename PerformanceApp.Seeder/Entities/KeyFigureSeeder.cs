using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Services;

namespace PerformanceApp.Seeder.Entities;

public class KeyFigureSeeder(PadbContext context)
{
    private readonly KeyFigureValueRepository _keyFigureValueRepository = new(context);
    private readonly IKeyFigureValueService _keyFigureValueService = new KeyFigureValueService(context);

    private async Task<bool> IsPopulated()
    {
        var keyFigureValues = await _keyFigureValueRepository.GetKeyFigureValuesAsync();

        return keyFigureValues.Any();
    }

    public async Task Seed()
    {
        var exists = await IsPopulated();

        if (exists)
        {
            return;
        }

        await _keyFigureValueService.UpdateStandardDeviationsAsync();
        await _keyFigureValueService.UpdateTrackingErrorsAsync();
        await _keyFigureValueService.UpdateAnnualisedCumulativeReturnsAsync();
        await _keyFigureValueService.UpdateInformationRatiosAsync();
        await _keyFigureValueService.UpdateHalfYearPerformancesAsync();

    }
}