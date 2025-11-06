using PerformanceApp.Server.Models;
using PerformanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Server.Repositories
{
    public interface IBenchmarkRepository
    {
        Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync();
    }

    public class BenchmarkRepository(PadbContext context) : IBenchmarkRepository
    {
        private readonly PadbContext _context = context;

        public async Task<IEnumerable<Benchmark>> GetBenchmarkMappingsAsync()
            => await _context.Benchmarks
                .Include(b => b.Portfolio)
                .Include(b => b.BenchmarkNavigation)
                .ToListAsync();

    }
}