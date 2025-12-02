using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Repositories
{
    public interface IPortfolioRepository
    {
        Task AddPortfoliosAsync(List<Portfolio> portfolios);
        Task<Portfolio?> GetPortfolioAsync(int portfolioId);
        Task<IEnumerable<Portfolio>> GetProperPortfoliosAsync();
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync(List<string> names);
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync();
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync(string userId);
    }
    public class PortfolioRepository(PadbContext context) : IPortfolioRepository
    {
        private readonly PadbContext _context = context;

        public async Task AddPortfoliosAsync(List<Portfolio> portfolios)
        {
            await _context.Portfolios.AddRangeAsync(portfolios);
            await _context.SaveChangesAsync();
        }
        public async Task<Portfolio?> GetPortfolioAsync(int portfolioId)
        {
            return await _context.Portfolios
                .Include(p => p.PortfolioPerformancesNavigation)
                    .ThenInclude(pp => pp.PerformanceTypeNavigation)
                .SingleOrDefaultAsync(p => p.Id == portfolioId);
        }
        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(List<string> names)
        {
            return await _context.Portfolios
                .Where(p => names.Contains(p.Name))
                .OfType<Portfolio>()
                .ToListAsync();
        }
        private bool IsProperPortfolio(Portfolio p)
        {
            return _context.Benchmarks
                    .Select(b => b.PortfolioId)
                    .Contains(p.Id);
        }
        public async Task<IEnumerable<Portfolio>> GetProperPortfoliosAsync()
        {
            var portfolios = await _context.Portfolios
                .Include(p => p.BenchmarkPortfoliosNavigation)
                    .ThenInclude(b => b.BenchmarkPortfolioNavigation)
                .Include(p => p.BenchmarkBenchmarksNavigation)
                    .ThenInclude(b => b.PortfolioPortfolioNavigation)
                .ToListAsync();

            return portfolios.Where(IsProperPortfolio);
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
        {
            return await _context.Portfolios
                .Include(p => p.BenchmarkPortfoliosNavigation)
                    .ThenInclude(b => b.BenchmarkPortfolioNavigation)
                .Include(p => p.BenchmarkBenchmarksNavigation)
                    .ThenInclude(b => b.PortfolioPortfolioNavigation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(string userId)
        {
            var properPortfolios = await GetProperPortfoliosAsync();
            return properPortfolios.Where(p => p.UserID == userId).ToList();
        }

    }
}