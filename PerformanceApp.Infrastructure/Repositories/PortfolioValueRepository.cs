using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IPortfolioValueRepository
{
    Task<IEnumerable<PortfolioValue>> GetPortfolioValuesAsync();
    Task AddPortfolioValuesAsync(IEnumerable<PortfolioValue> portfolioValues);
}

public class PortfolioValueRepository(PadbContext context) : IPortfolioValueRepository
{
    private readonly PadbContext _context = context;

    public async Task<IEnumerable<PortfolioValue>> GetPortfolioValuesAsync()
    {
        return await _context.PortfolioValues.ToListAsync();
    }
    public async Task AddPortfolioValuesAsync(IEnumerable<PortfolioValue> portfolioValues)
    {
        await _context.PortfolioValues.AddRangeAsync(portfolioValues);
        await _context.SaveChangesAsync();
    }
}