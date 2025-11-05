using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Repositories
{
    public interface IDateInfoRepository
    {
        Task<IEnumerable<DateInfo>> GetDateInfosAsync();
    }

    public class DateInfoRepository : IDateInfoRepository
    {
        private readonly PadbContext _context;

        public DateInfoRepository(PadbContext context)
        {
            _context = context;
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