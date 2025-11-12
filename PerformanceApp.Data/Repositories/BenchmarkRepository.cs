using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PerformanceApp.Data.Repositories
{
    public interface IBenchmarkRepository
    {
        EntityEntry<Benchmark>? AddBenchmarkMapping(Benchmark benchmark);
        List<EntityEntry<Benchmark>?> AddBenchmarkMappings(List<Benchmark> benchmark);
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync();
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsWithKeyFiguresAsync(int portfolioId);
    }

    public class BenchmarkRepository(PadbContext context) : IBenchmarkRepository
    {
        private readonly PadbContext _context = context;

        private static bool Equal(Benchmark lhs, Benchmark rhs)
        {
            return lhs.PortfolioId == rhs.PortfolioId && lhs.BenchmarkId == lhs.BenchmarkId;
        }

        private bool Exists(Benchmark benchmark)
        {
            return _context.Benchmarks.Any(b => Equal(b, benchmark));
        }
        public EntityEntry<Benchmark>? AddBenchmarkMapping(Benchmark benchmark)
        {
            if (Exists(benchmark))
            {
                return null;
            }
            return _context.Add(benchmark);
        }
        public List<EntityEntry<Benchmark>?> AddBenchmarkMappings(List<Benchmark> benchmarks)
            => benchmarks.Select(AddBenchmarkMapping).ToList();

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync()
            => await _context.Benchmarks
                .Include(b => b.PortfolioPortfolioNavigation)
                .Include(b => b.BenchmarkPortfolioNavigation)
                .ToListAsync();

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsWithKeyFiguresAsync(int portfolioId)
            => await _context.Benchmarks
                .Include(bm => bm.PortfolioPortfolioNavigation)
                    .ThenInclude(p => p.KeyFigureValuesNavigation)
                        .ThenInclude(kfv => kfv.KeyFigureInfoNavigation)
                .Include(bm => bm.BenchmarkPortfolioNavigation)
                    .ThenInclude(b => b.KeyFigureValuesNavigation)
                        .ThenInclude(kfv => kfv.KeyFigureInfoNavigation)
                .Where(bm => bm.PortfolioPortfolioNavigation.PortfolioId == portfolioId)
                .ToListAsync();

    }
}