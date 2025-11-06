using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Repositories
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync();
        Task<IEnumerable<Portfolio>> GetAllPortfoliosAsync();
    }
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PadbContext _context;

        public PortfolioRepository(PadbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
            => await _context.Portfolios
                .Where(
                    p => _context.Benchmarks
                            .Select(b => b.PortfolioId)
                            .Contains(p.PortfolioId)
                ).ToListAsync();
        public async Task<IEnumerable<Portfolio>> GetAllPortfoliosAsync()
            => await _context.Portfolios.ToListAsync();

    }
}