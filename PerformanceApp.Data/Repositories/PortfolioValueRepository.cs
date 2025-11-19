using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Repositories;

public interface IPortfolioValueRepository
{
    Task<IEnumerable<PortfolioValue>> GetPortfolioValuesAsync();
}

public class PortfolioValueRepository(PadbContext context) : IPortfolioValueRepository
{
    private readonly PadbContext _context = context;

    public async Task<IEnumerable<PortfolioValue>> GetPortfolioValuesAsync()
    {
        return await _context.PortfolioValues.ToListAsync();
    }
}