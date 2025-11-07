using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PerformanceApp.Server.Repositories
{
    public interface IPerformanceRepository
    {
        Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync();
        Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId);
        Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync();
    }

    public class PerformanceRepository(PadbContext context) : IPerformanceRepository
    {
        private readonly PadbContext _context = context;

        private IQueryable<KeyFigureValue> BaseKeyFigureValuesQuery()
            => _context.KeyFigureValues
                .Include(kfv => kfv.Portfolio)
                .AsQueryable();

        public async Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync()
            => await BaseKeyFigureValuesQuery()
                .ToListAsync();
        public async Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId)
            => await BaseKeyFigureValuesQuery()
                .Include(k => k.Portfolio)
                .ToListAsync();
        public async Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync()
            => await _context.KeyFigureInfos
                .ToListAsync();

    }
}