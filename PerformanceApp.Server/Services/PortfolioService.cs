using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;
using PerformanceApp.Server.Services.Mappers;
using PerformanceApp.Server.Services.Helpers;

namespace PerformanceApp.Server.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync();
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync(string userId);
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(string userId);
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId);
        Task<List<PortfolioPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId);
        Task<Benchmark?> GetBenchmarkAsync(int portfolioId);
        Task<List<PortfolioBenchmarkPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId);
    }

    public class PortfolioService(IPortfolioRepository portfolioRepository, IBenchmarkRepository benchmarkRepository) : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IBenchmarkRepository _benchmarkRepository = benchmarkRepository;

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync()
        {
            var portfolios = await _portfolioRepository.GetProperPortfoliosAsync();

            return portfolios
                .Select(PortfolioMapper.MapToPortfolioDTO)
                .ToList();
        }

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync(string userId)
        {
            var portfolios = await _portfolioRepository.GetPortfoliosAsync(userId);

            return portfolios
                .Select(PortfolioMapper.MapToPortfolioDTO)
                .ToList();
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(string userId)
        {
            var portfolios = await _portfolioRepository.GetPortfoliosAsync(userId);

            return portfolios
                .SelectMany(PortfolioMapper.MapToPortfolioBenchmarkDTOs)
                .ToList();
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync()
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();

            return benchmarkMappings
                .Select(BenchmarkMapper.MapToPortfolioBenchmarkDTO)
                .ToList();
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId)
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();

            return benchmarkMappings
                .Where(b => b.PortfolioId == portfolioId)
                .Select(BenchmarkMapper.MapToPortfolioBenchmarkDTO)
                .ToList();
        }

        public async Task<List<PortfolioPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(portfolioId);

            if (portfolio == null)
            {
                return [];
            }

            return portfolio.PortfolioPerformancesNavigation
                .Where(PortfolioPerformanceHelper.IsCumulativeDayPerformance)
                .Select(PortfolioPerformanceMapper.MapToPortfolioPerformanceDTO)
                .ToList();
        }

        public async Task<Benchmark?> GetBenchmarkAsync(int portfolioId)
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();
            var benchmark = benchmarkMappings.SingleOrDefault(b => b.PortfolioId == portfolioId);

            return benchmark;
        }

        public async Task<List<PortfolioBenchmarkPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var benchmark = await GetBenchmarkAsync(portfolioId);

            if (benchmark == null)
            {
                return [];
            }

            var benchmarkId = benchmark.BenchmarkId;

            var portfolioPerformance = await GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);
            var benchmarkPerformance = await GetPortfolioCumulativeDayPerformanceDTOsAsync(benchmarkId);

            var empty = !portfolioPerformance.Any() || !benchmarkPerformance.Any();
            if (empty)
            {
                return [];
            }

            return portfolioPerformance
                .Join(
                    benchmarkPerformance,
                    PortfolioPerformanceHelper.GetBankday,
                    PortfolioPerformanceHelper.GetBankday,
                    PortfolioPerformanceMapper.MapToPortfolioBenchmarkPerformanceDTO
                )
                .OrderBy(dto => dto.Bankday)
                .ToList();
        }
    }

}