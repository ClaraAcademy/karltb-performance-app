using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PerformanceApp.Data.Repositories
{
    public interface IKeyFigureValueRepository
    {
        Task<IEnumerable<KeyFigureValue>> GetKeyFigureValuesAsync(int portfolioId);
        Task<IEnumerable<KeyFigureInfo>> GetKeyFigureInfosAsync(); // This is in the wrong class
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

    }
}