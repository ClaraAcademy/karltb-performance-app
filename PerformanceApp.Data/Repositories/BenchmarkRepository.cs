using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Repositories
{
    public interface IBenchmarkRepository
    {
        Task AddBenchmarkMappingAsync(Benchmark benchmark);
        Task AddBenchmarkMappingsAsync(List<Benchmark> benchmarks);
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync();
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsWithKeyFiguresAsync(int portfolioId);
    }

    public class BenchmarkRepository(PadbContext context) : IBenchmarkRepository
    {
        private readonly PadbContext _context = context;

        public async Task AddBenchmarkMappingAsync(Benchmark benchmark)
        {
            await _context.Benchmarks.AddAsync(benchmark);
            await _context.SaveChangesAsync();
        }

        public async Task AddBenchmarkMappingsAsync(List<Benchmark> benchmarks)
        {
            await _context.Benchmarks.AddRangeAsync(benchmarks);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync()
        {
            return await _context.Benchmarks
                   .Include(b => b.PortfolioPortfolioNavigation)
                   .Include(b => b.BenchmarkPortfolioNavigation)
                   .ToListAsync();
        }

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsWithKeyFiguresAsync(int portfolioId)
            => await _context.Benchmarks
                .Include(bm => bm.PortfolioPortfolioNavigation)
                    .ThenInclude(p => p.KeyFigureValuesNavigation)
                        .ThenInclude(kfv => kfv.KeyFigureInfoNavigation)
                .Include(bm => bm.BenchmarkPortfolioNavigation)
                    .ThenInclude(b => b.KeyFigureValuesNavigation)
                        .ThenInclude(kfv => kfv.KeyFigureInfoNavigation)
                .Where(bm => bm.PortfolioPortfolioNavigation.Id == portfolioId)
                .ToListAsync();

    }
}