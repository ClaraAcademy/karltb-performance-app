using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Context;

namespace PerformanceApp.Infrastructure.Repositories;

public interface IPortfolioPerformanceRepository
{
    Task<IEnumerable<PortfolioPerformance>> GetPortfolioPerformancesAsync();
    Task AddPortfolioPerformancesAsync(IEnumerable<PortfolioPerformance> portfolioPerformances);
}

public class PortfolioPerformanceRepository(PadbContext context) : IPortfolioPerformanceRepository
{
    private readonly PadbContext _context = context;

    public async Task<IEnumerable<PortfolioPerformance>> GetPortfolioPerformancesAsync()
    {
        return await _context.PortfolioPerformances.ToListAsync();
    }
    public async Task AddPortfolioPerformancesAsync(IEnumerable<PortfolioPerformance> portfolioPerformances)
    {
        await _context.PortfolioPerformances.AddRangeAsync(portfolioPerformances);
        await _context.SaveChangesAsync();
    }
}