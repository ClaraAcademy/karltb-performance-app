using PerformanceApp.Server.Models;
using PerformanceApp.Server.Repositories;

namespace PerformanceApp.Server.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId);
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IBenchmarkRepository _benchmarkRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository, IBenchmarkRepository benchmarkRepository)
        {
            _portfolioRepository = portfolioRepository;
            _benchmarkRepository = benchmarkRepository;
        }

        private static PortfolioDTO MapToPortfolioDTO(Portfolio p)
            => new PortfolioDTO { PortfolioId = p.PortfolioId, PortfolioName = p.PortfolioName };

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync()
            => (await _portfolioRepository.GetPortfoliosAsync())
                .Select(MapToPortfolioDTO)
                .ToList();

        private static PortfolioBenchmarkDTO MapToPortfolioBenchmarkDTO(Benchmark b)
            => new PortfolioBenchmarkDTO
            {
                PortfolioId = b.Portfolio.PortfolioId,
                PortfolioName = b.Portfolio.PortfolioName,
                BenchmarkId = b.BenchmarkNavigation.PortfolioId,
                BenchmarkName = b.BenchmarkNavigation.PortfolioName
            };

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync()
            => (await _benchmarkRepository.GetBenchmarkMappingsAsync())
                .Select(MapToPortfolioBenchmarkDTO)
                .ToList();

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId)
            => (await _benchmarkRepository.GetBenchmarkMappingsAsync())
                .Where(b => b.PortfolioId == portfolioId)
                .Select(MapToPortfolioBenchmarkDTO)
                .ToList();

    }

}