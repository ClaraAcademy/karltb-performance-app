using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Services;

public interface IDateInfoService
{
    Task<DateOnly> GetPreviousBankdayAsync(DateOnly date);
    Task<decimal> GetAnnualizationFactorAsync();
}

public class DateInfoService(IDateInfoRepository dateInfoRepository) : IDateInfoService
{
    private readonly IDateInfoRepository _dateInfoRepository = dateInfoRepository;
    private const decimal BankdaysPerYear = 250;

    public async Task<DateOnly> GetPreviousBankdayAsync(DateOnly date)
    {
        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();

        var previousBankday = dateInfos
            .Where(d => d.Bankday < date)
            .OrderByDescending(d => d.Bankday)
            .FirstOrDefault();

        if (previousBankday == null)
        {
            return BankdayData.FirstDay;
        }

        return previousBankday.Bankday;

    }
    public async Task<decimal> GetAnnualizationFactorAsync()
    {
        var dateInfos = await _dateInfoRepository.GetDateInfosAsync();
        var n = dateInfos.Count();

        return n == 0 ? 0M : BankdaysPerYear / n;
    }
}