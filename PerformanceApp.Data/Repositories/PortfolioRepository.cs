using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PerformanceApp.Data.Repositories
{
    public interface IPortfolioRepository
    {
        EntityEntry<Portfolio>? AddPortfolio(Portfolio portfolio);
        List<EntityEntry<Portfolio>?> AddPortfolios(List<Portfolio> portfolio);
        Portfolio? GetPortfolio(string name);
        Task<Portfolio?> GetPortfolioAsync(string name);
        Task<Portfolio> GetPortfolioAsync(int portfolioId);
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync();
        Task<IEnumerable<Portfolio>> GetAllPortfoliosAsync();
    }
    public class PortfolioRepository(PadbContext context) : IPortfolioRepository
    {
        private readonly PadbContext _context = context;
        private static bool Equal(Portfolio lhs, Portfolio rhs)
            => lhs.PortfolioName == rhs.PortfolioName && lhs.UserID == rhs.UserID;
        private bool Exists(Portfolio portfolio)
        {
            return _context.Portfolios.AsEnumerable().Any(p => Equal(p, portfolio));
        }

        public Portfolio? GetPortfolio(string name)
        {
            return _context.Portfolios.SingleOrDefault(p => p.PortfolioName == name);
        }

        public async Task<Portfolio?> GetPortfolioAsync(string name)
        {
            return await _context.Portfolios.SingleOrDefaultAsync(p => p.PortfolioName == name);
        }

        public EntityEntry<Portfolio>? AddPortfolio(Portfolio portfolio)
            => Exists(portfolio) ? null
                                 : _context.Portfolios.Add(portfolio);

        public List<EntityEntry<Portfolio>?> AddPortfolios(List<Portfolio> portfolios)
            => portfolios.Select(AddPortfolio).ToList();

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