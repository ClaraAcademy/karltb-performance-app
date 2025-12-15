using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Infrastructure.Repositories
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
            var portfolios = await GetPortfoliosAsync();

            return portfolios.SingleOrDefault(p => p.Id == portfolioId);
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(List<string> names)
        {
            var portfolios = await GetPortfoliosAsync();

            return portfolios
                .Where(p => names.Contains(p.Name))
                .OfType<Portfolio>()
                .ToList();
        }

        public async Task<IEnumerable<Portfolio>> GetProperPortfoliosAsync()
        {
            var portfolios = await GetPortfoliosAsync();

            var filtered = portfolios.Where(p => p.BenchmarksNavigation.Count() > 0);

            return filtered;
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
        {
            return await _context.Portfolios
                .Include(p => p.KeyFigureValuesNavigation)
                    .ThenInclude(kfv => kfv.KeyFigureInfoNavigation)
                .Include(p => p.PortfolioPerformancesNavigation)
                    .ThenInclude(pp => pp.PerformanceTypeNavigation)
                .Include(p => p.PortfolioPortfolioBenchmarkEntityNavigation)
                    .ThenInclude(b => b.BenchmarkPortfolioNavigation)
                .Include(p => p.BenchmarkPortfolioBenchmarkEntityNavigation)
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