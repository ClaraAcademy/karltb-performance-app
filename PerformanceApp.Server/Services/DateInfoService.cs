using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Server.Services
{
    public interface IDateInfoService
    {
        Task<List<BankdayDTO>> GetBankdayDTOsAsync();
    }

    public class DateInfoService(IDateInfoRepository repo) : IDateInfoService
    {
        private readonly IDateInfoRepository _repo = repo;

        public async Task<List<BankdayDTO>> GetBankdayDTOsAsync()
        {
            var dateInfos = await _repo.GetDateInfosAsync();

            return dateInfos
                .Select(
                    di => new BankdayDTO
                    {
                        Bankday = di.Bankday
                    }
                ).ToList();
        }
    }
}