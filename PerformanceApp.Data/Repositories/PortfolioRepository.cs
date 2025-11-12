using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace PerformanceApp.Data.Repositories
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetPortfolioAsync(int portfolioId);
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync();
        Task<IEnumerable<Portfolio>> GetAllPortfoliosAsync();
    }
    public class PortfolioRepository(PadbContext context) : IPortfolioRepository
    {
        private readonly PadbContext _context = context;

        public async Task<Portfolio> GetPortfolioAsync(int portfolioId)
            => await _context.Portfolios
                .Include(p => p.PortfolioCumulativeDayPerformancesNavigation)
                .SingleAsync(p => p.PortfolioId == portfolioId);

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
            => await _context.Portfolios
                .Where(
                    p => _context.Benchmarks
                            .Select(b => b.PortfolioId)
                            .Contains(p.PortfolioId)
                ).ToListAsync();
        public async Task<IEnumerable<Portfolio>> GetAllPortfoliosAsync()
            => await _context.Portfolios
                .ToListAsync();

    }
}