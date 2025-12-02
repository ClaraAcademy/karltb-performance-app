using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IKeyFigureInfoService
{
    Task<int> GetIdAsync(string name);
}

public class KeyFigureInfoService(KeyFigureInfoRepository keyFigureInfoRepository) : IKeyFigureInfoService
{
    private readonly KeyFigureInfoRepository _keyFigureInfoRepository = keyFigureInfoRepository;

    public async Task<int> GetIdAsync(string name)
    {
        var keyFigureInfos = await _keyFigureInfoRepository.GetKeyFigureInfosAsync();

        var keyFigureInfo = keyFigureInfos.FirstOrDefault(kf => kf.Name == name)
            ?? throw new KeyNotFoundException($"KeyFigureInfo with name '{name}' not found.");

        return keyFigureInfo.Id;

    }
}