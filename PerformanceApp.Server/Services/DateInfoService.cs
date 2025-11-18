using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Services
{
    public interface IDateInfoService
    {
        Task<List<BankdayDTO>> GetBankdayDTOsAsync();
    }

    public class DateInfoService(IDateInfoRepository repo) : IDateInfoService
    {
        private readonly IDateInfoRepository _repo = repo;

        private BankdayDTO MapToDTO(DateInfo dateInfo)
        {
            return new BankdayDTO { Bankday = dateInfo.Bankday };
        }

        public async Task<List<BankdayDTO>> GetBankdayDTOsAsync()
        {
            var dateInfos = await _repo.GetDateInfosAsync();

            return dateInfos.Select(MapToDTO).ToList();
        }
    }
}