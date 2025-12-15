using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Services;

public interface IKeyFigureInfoService
{
    Task<int> GetIdAsync(string name);
    Task<int> GetStandardDeviationIdAsync();
    Task<int> GetTrackingErrorIdAsync();
    Task<int> GetAnnualizedCumulativeReturnIdAsync();
    Task<int> GetInformationRatioIdAsync();
    Task<int> GetHalfYearPerformanceIdAsync();
}

public class KeyFigureInfoService(PadbContext context) : IKeyFigureInfoService
{
    private readonly KeyFigureInfoRepository _keyFigureInfoRepository = new KeyFigureInfoRepository(context);

    public async Task<int> GetIdAsync(string name)
    {
        var keyFigureInfos = await _keyFigureInfoRepository.GetKeyFigureInfosAsync();

        var keyFigureInfo = keyFigureInfos.FirstOrDefault(kf => kf.Name == name)
            ?? throw new KeyNotFoundException($"KeyFigureInfo with name '{name}' not found.");

        return keyFigureInfo.Id;
    }
    public async Task<int> GetStandardDeviationIdAsync()
    {
        return await GetIdAsync(KeyFigureData.StandardDeviation);
    }
    public async Task<int> GetTrackingErrorIdAsync()
    {
        return await GetIdAsync(KeyFigureData.TrackingError);
    }
    public async Task<int> GetAnnualizedCumulativeReturnIdAsync()
    {
        return await GetIdAsync(KeyFigureData.AnnualisedCumulativeReturn);
    }
    public async Task<int> GetInformationRatioIdAsync()
    {
        return await GetIdAsync(KeyFigureData.InformationRatio);
    }
    public async Task<int> GetHalfYearPerformanceIdAsync()
    {
        return await GetIdAsync(KeyFigureData.HalfYearPerformance);
    }
}