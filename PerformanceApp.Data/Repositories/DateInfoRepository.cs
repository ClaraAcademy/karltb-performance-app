using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Repositories
{
    public interface IDateInfoRepository
    {
        Task<IEnumerable<DateInfo>> GetDateInfosAsync();
    }

    public class DateInfoRepository(PadbContext context) : IDateInfoRepository
    {
        private readonly PadbContext _context = context;

        public async Task<IEnumerable<DateInfo>> GetDateInfosAsync()
        {
            return await _context.DateInfos
                .Distinct()
                .OrderBy(d => d.Bankday)
                .ToListAsync();
        }

    }
}