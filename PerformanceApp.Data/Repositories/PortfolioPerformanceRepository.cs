using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Repositories;

public interface IPortfolioPerformanceRepository
{
    Task<IEnumerable<PortfolioPerformance>> GetPortfolioPerformancesAsync();
}

public class PortfolioPerformanceRepository(PadbContext context) : IPortfolioPerformanceRepository
{
    private readonly PadbContext _context = context;

    public async Task<IEnumerable<PortfolioPerformance>> GetPortfolioPerformancesAsync()
    {
        return await _context.PortfolioPerformances.ToListAsync();
    }
}