using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPerformanceService
{
    Task<int> GetPerformanceIdAsync(string name);
    Task<int> GetCumulativeDayPerformanceIdAsync();
    Task<int> GetDayPerformanceIdAsync();
    Task<int> GetHalfYearPerformanceIdAsync();
    Task<int> GetMonthPerformanceIdAsync();
    decimal GetPerformanceValue(decimal current, decimal previous);
}

public class PerformanceService(IPerformanceTypeRepository performanceTypeRepository) : IPerformanceService
{
    private readonly IPerformanceTypeRepository _performanceTypeRepository = performanceTypeRepository;
    public async Task<int> GetPerformanceIdAsync(string name)
    {
        var performances = await _performanceTypeRepository.GetPerformanceTypeInfosAsync();

        var performance = performances.FirstOrDefault(p => p.Name == name)
            ?? throw new KeyNotFoundException($"PerformanceType with name '{name}' not found.");

        return performance.Id;
    }
    public async Task<int> GetCumulativeDayPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeData.CumulativeDayPerformance);
    }
    public async Task<int> GetDayPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeData.DayPerformance);
    }
    public async Task<int> GetHalfYearPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeData.HalfYearPerformance);
    }
    public async Task<int> GetMonthPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeData.MonthPerformance);
    }
    public decimal GetPerformanceValue(decimal current, decimal previous)
    {
        if (previous == 0M)
        {
            return 0M;
        }
        return current / previous - 1M;
    }
}