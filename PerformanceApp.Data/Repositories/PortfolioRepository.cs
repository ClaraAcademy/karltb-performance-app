using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Repositories
{
    public interface IPortfolioRepository
    {
        void AddPortfolio(Portfolio portfolio);
        Task AddPortfolioAsync(Portfolio portfolio);
        void AddPortfolios(List<Portfolio> portfolios);
        Task AddPortfoliosAsync(List<Portfolio> portfolios);
        Portfolio? GetPortfolio(string name);
        Task<Portfolio?> GetPortfolioAsync(string name);
        Task<Portfolio> GetPortfolioAsync(int portfolioId);
        Task<IEnumerable<Portfolio>> GetProperPortfoliosAsync();
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync(List<string> names);
        IEnumerable<Portfolio> GetPortfolios();
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync();
    }
    public class PortfolioRepository(PadbContext context) : IPortfolioRepository
    {
        private readonly PadbContext _context = context;

        public Portfolio? GetPortfolio(string name)
        {
            return _context.Portfolios.SingleOrDefault(p => p.Name == name);
        }

        public async Task<Portfolio?> GetPortfolioAsync(string name)
        {
            return await _context.Portfolios.SingleOrDefaultAsync(p => p.Name == name);
        }

        public void AddPortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
            _context.SaveChanges();
        }
        public async Task AddPortfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
        }

        public void AddPortfolios(List<Portfolio> portfolios)
        {
            _context.Portfolios.AddRange(portfolios);
            _context.SaveChanges();
        }

        public async Task AddPortfoliosAsync(List<Portfolio> portfolios)
        {
            await _context.Portfolios.AddRangeAsync(portfolios);
            await _context.SaveChangesAsync();
        }

        public async Task<Portfolio> GetPortfolioAsync(int portfolioId)
        {
            return await _context.Portfolios
                .Include(p => p.PortfolioPerformancesNavigation)
                .SingleAsync(p => p.Id == portfolioId);
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(List<string> names)
        {
            return await _context.Portfolios
                .Where(p => names.Contains(p.Name))
                .OfType<Portfolio>()
                .ToListAsync();
        }

        public async Task<IEnumerable<Portfolio>> GetProperPortfoliosAsync()
            => await _context.Portfolios
                .Where(
                    p => _context.Benchmarks
                            .Select(b => b.PortfolioId)
                            .Contains(p.Id)
                ).ToListAsync();

        public IEnumerable<Portfolio> GetPortfolios()
        {
            return _context.Portfolios.ToList();
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
        {
            return await _context.Portfolios.ToListAsync();
        }

    }
}