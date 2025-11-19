using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Repositories
{
    public interface IDateInfoRepository
    {
        Task AddDateInfosAsync(List<DateInfo> dateInfos);
        Task<IEnumerable<DateInfo>> GetDateInfosAsync();
    }

    public class DateInfoRepository(PadbContext context) : IDateInfoRepository
    {
        private readonly PadbContext _context = context;

        public async Task AddDateInfosAsync(List<DateInfo> dateInfos)
        {
            await _context.DateInfos.AddRangeAsync(dateInfos);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DateInfo>> GetDateInfosAsync()
        {
            return await _context.DateInfos
                .Distinct()
                .OrderBy(d => d.Bankday)
                .ToListAsync();
        }

    }
}