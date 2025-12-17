using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Constants;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IPositionRepository
{
    Task<IEnumerable<Position>> GetPositionsAsync();
    Task<IEnumerable<Position>> GetStockPositionsAsync(DateOnly bankday, int portfolioId);
    Task<IEnumerable<Position>> GetBondPositionsAsync(DateOnly bankday, int portfolioId);
    Task<IEnumerable<Position>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId);
    Task AddPositionsAsync(IEnumerable<Position> positions);
}

public class PositionRepository(PadbContext context) : IPositionRepository
{
    private readonly PadbContext _context = context;

    private async Task<IEnumerable<Position>> GetPositionsAsync(
        string instrumentType,
        DateOnly bankday,
        int portfolioId
    )
    {
        return await _context.Positions
            .Include(p => p.InstrumentNavigation!)
                .ThenInclude(i => i.InstrumentTypeNavigation)
            .Include(p => p.InstrumentNavigation!)
                .ThenInclude(i => i.InstrumentPricesNavigation)
            .Include(p => p.PositionValuesNavigation)
            .Where(p => p.Bankday == bankday)
            .Where(p => p.PortfolioId == portfolioId)
            .Where(p => p.InstrumentNavigation!.InstrumentTypeNavigation!.Name == instrumentType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Position>> GetStockPositionsAsync(DateOnly bankday, int portfolioId)
    {
        var stock = InstrumentTypeConstants.Stock;
        return await GetPositionsAsync(stock, bankday, portfolioId);
    }

    public async Task<IEnumerable<Position>> GetBondPositionsAsync(DateOnly bankday, int portfolioId)
    {
        var bond = InstrumentTypeConstants.Bond;
        return await GetPositionsAsync(bond, bankday, portfolioId);
    }

    public async Task<IEnumerable<Position>> GetIndexPositionsAsync(DateOnly bankday, int portfolioId)
    {
        var index = InstrumentTypeConstants.Index;
        return await GetPositionsAsync(index, bankday, portfolioId);
    }

    public async Task<IEnumerable<Position>> GetPositionsAsync()
    {
        return await _context.Positions.ToListAsync();
    }
    public async Task AddPositionsAsync(IEnumerable<Position> positions)
    {
        await _context.Positions.AddRangeAsync(positions);
        await _context.SaveChangesAsync();
    }
}