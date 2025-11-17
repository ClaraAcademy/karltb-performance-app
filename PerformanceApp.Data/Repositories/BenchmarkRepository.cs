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

    }
}