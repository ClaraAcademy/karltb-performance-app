using PerformanceApp.Data.Repositories;
using PerformanceApp.Server.Services.Mappers;
using PerformanceApp.Data.Dtos;

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
                .Select(DateInfoMapper.MapToBankdayDto)
                .ToList();
        }
    }
}