using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PerformanceApp.Data.Repositories
{
    public interface IKeyFigureValueRepository
    {
        Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId);
        Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync();
    }

    public class KeyFigureValueRepository(PadbContext context) : IKeyFigureValueRepository
    {
        private readonly PadbContext _context = context;

        private IQueryable<KeyFigureValue> BaseKeyFigureValuesQuery()
        {
            return _context.KeyFigureValues
                   .Include(kfv => kfv.PortfolioNavigation)
                   .AsQueryable();
        }

        public async Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId)
        {
            return await BaseKeyFigureValuesQuery()
                   .Include(k => k.PortfolioNavigation)
                   .ToListAsync();
        }
        public async Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync()
        {
            return await _context.KeyFigureInfos.ToListAsync();
        }

    }
}