using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Repositories
{
    public interface IBenchmarkRepository
    {
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync();
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsWithKeyFiguresAsync(int portfolioId);
    }

    public class BenchmarkRepository(PadbContext context) : IBenchmarkRepository
    {
        private readonly PadbContext _context = context;

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync()
            => await _context.Benchmarks
                .Include(b => b.PortfolioNavigation)
                .Include(b => b.BenchmarkNavigation)
                .ToListAsync();

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsWithKeyFiguresAsync(int portfolioId)
            => await _context.Benchmarks
                .Include(bm => bm.PortfolioNavigation)
                    .ThenInclude(p => p.KeyFigureValues)
                        .ThenInclude(kfv => kfv.KeyFigureInfo)
                .Include(bm => bm.BenchmarkNavigation)
                    .ThenInclude(b => b.KeyFigureValues)
                        .ThenInclude(kfv => kfv.KeyFigureInfo)
                .Where(bm => bm.PortfolioNavigation.PortfolioId == portfolioId)
                .ToListAsync();

    }
}