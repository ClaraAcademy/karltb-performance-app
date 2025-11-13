using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PerformanceApp.Data.Repositories
{
    public interface IDateInfoRepository
    {
        EntityEntry<DateInfo>? AddDateInfo(DateInfo dateInfo);
        List<EntityEntry<DateInfo>?> AddDateInfos(List<DateInfo> dateInfos);
        IEnumerable<DateInfo> GetDateInfos();
        Task<IEnumerable<DateInfo>> GetDateInfosAsync();
    }

    public class DateInfoRepository(PadbContext context) : IDateInfoRepository
    {
        private readonly PadbContext _context = context;

        private static bool Equal(DateInfo lhs, DateInfo rhs) => lhs.Bankday == rhs.Bankday;
        private bool Exists(DateInfo dateInfo)
        {
            return _context.DateInfos.AsEnumerable().Any(di => Equal(di, dateInfo));
        } 

        public EntityEntry<DateInfo>? AddDateInfo(DateInfo dateInfo)
        {
            return Exists(dateInfo) ? null : _context.DateInfos.Add(dateInfo);
        }
        public List<EntityEntry<DateInfo>?> AddDateInfos(List<DateInfo> dateInfos)
        {
            return dateInfos.Select(AddDateInfo).ToList();
        }

        public IEnumerable<DateInfo> GetDateInfos()
        {
            return _context.DateInfos.ToList();
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