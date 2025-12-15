using PerformanceApp.Data.Constants.PerformanceType;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Services;

public interface IPerformanceService
{
    Task<int> GetPerformanceIdAsync(string name);
    Task<int> GetCumulativeDayPerformanceIdAsync();
    Task<int> GetDayPerformanceIdAsync();
    Task<int> GetHalfYearPerformanceIdAsync();
    Task<int> GetMonthPerformanceIdAsync();
    decimal GetPerformanceValue(decimal current, decimal previous);
}

public class PerformanceService(PadbContext context) : IPerformanceService
{
    private readonly IPerformanceTypeRepository _performanceTypeRepository = new PerformanceTypeRepository(context);

    public async Task<int> GetPerformanceIdAsync(string name)
    {
        var performances = await _performanceTypeRepository.GetPerformanceTypeInfosAsync();

        var performance = performances.FirstOrDefault(p => p.Name == name)
            ?? throw new KeyNotFoundException($"PerformanceType with name '{name}' not found.");

        return performance.Id;
    }
    public async Task<int> GetCumulativeDayPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeConstants.CumulativeDay);
    }
    public async Task<int> GetDayPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeConstants.Day);
    }
    public async Task<int> GetHalfYearPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeConstants.HalfYear);
    }
    public async Task<int> GetMonthPerformanceIdAsync()
    {
        return await GetPerformanceIdAsync(PerformanceTypeConstants.Month);
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