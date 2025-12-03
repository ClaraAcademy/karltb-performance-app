using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Repositories
{
    public interface IKeyFigureValueRepository
    {
        Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId);
        Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync(); // This is in the wrong class
        Task AddKeyFigureValuesAsync(IEnumerable<KeyFigureValue> keyFigureValues);
    }

    public class KeyFigureValueRepository(PadbContext context) : IKeyFigureValueRepository
    {
        private readonly PadbContext _context = context;

        public async Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId)
        {
            return await _context.KeyFigureValues
                   .Where(kfv => kfv.PortfolioId == portfolioId)
                   .Include(kfv => kfv.PortfolioNavigation)
                   .ToListAsync();
        }
        public async Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync()
        {
            return await _context.KeyFigureInfos.ToListAsync();
        }
        public async Task AddKeyFigureValuesAsync(IEnumerable<KeyFigureValue> keyFigureValues)
        {
            await _context.KeyFigureValues.AddRangeAsync(keyFigureValues);
            await _context.SaveChangesAsync();
        }

    }
}