using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Repositories
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetStockPositionsAsync(DateOnly bankday, int portfolioId);
        Task<IEnumerable<Position>> GetBondPositionsAsync(DateOnly bankday, int portfolioId);
        Task<IEnumerable<Position>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId);
    }

    public class PositionRepository(PadbContext context) : IPositionRepository
    {
        private readonly PadbContext _context = context;

        private async Task<IEnumerable<Position>> GetPositionsAsync(
            string instrumentType,
            DateOnly bankday,
            int portfolioId
        )
            => await _context.Positions
                .Include(p => p.InstrumentNavigation!)
                    .ThenInclude(i => i.InstrumentTypeNavigation)
                .Include(p => p.InstrumentNavigation!)
                    .ThenInclude(i => i.InstrumentPricesNavigation)
                .Include(p => p.PositionValuesNavigation)
                .Where(p => p.Bankday == bankday)
                .Where(p => p.PortfolioId == portfolioId)
                .Where(p => p.InstrumentNavigation!.InstrumentTypeNavigation!.InstrumentTypeName == instrumentType)
                .ToListAsync();

        public async Task<IEnumerable<Position>> GetStockPositionsAsync(DateOnly bankday, int portfolioId)
            => await GetPositionsAsync("Stock", bankday, portfolioId);
        public async Task<IEnumerable<Position>> GetBondPositionsAsync(DateOnly bankday, int portfolioId)
            => await GetPositionsAsync("Bond", bankday, portfolioId);
        public async Task<IEnumerable<Position>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId)
            => await GetPositionsAsync("Index", bankday, portfolioId);

    }
}