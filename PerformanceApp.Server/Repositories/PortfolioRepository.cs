using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Repositories
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>> GetPortfoliosAsync();
    }
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly PadbContext _context;

        public PortfolioRepository(PadbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync()
        {
            return await _context.Portfolios
                .Where(
                    p => _context.Benchmarks
                        .Select(b => b.PortfolioId)
                        .Contains(p.PortfolioId)
                ).ToListAsync();
        }
    }
}