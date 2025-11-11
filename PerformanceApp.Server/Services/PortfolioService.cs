using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using PerformanceApp.Server.Models;
using PerformanceApp.Server.DTOs;
using PerformanceApp.Server.Repositories;

namespace PerformanceApp.Server.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId);
        Task<List<PortfolioCumulativeDayPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync();
        Task<List<PortfolioCumulativeDayPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId);
        Task<List<PortfolioBenchmarkCumulativeDayPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId);
    }

    public class PortfolioService(IPortfolioRepository portfolioRepository, IBenchmarkRepository benchmarkRepository) : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IBenchmarkRepository _benchmarkRepository = benchmarkRepository;

        private static PortfolioDTO MapToPortfolioDTO(Portfolio p)
            => new PortfolioDTO { PortfolioId = p.PortfolioId, PortfolioName = p.PortfolioName };

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync()
            => (await _portfolioRepository.GetPortfoliosAsync())
                .Select(MapToPortfolioDTO)
                .ToList();

        private static PortfolioBenchmarkDTO MapToPortfolioBenchmarkDTO(Benchmark b)
            => new PortfolioBenchmarkDTO
            {
                PortfolioId = b.PortfolioPortfolioNavigation.PortfolioId,
                PortfolioName = b.PortfolioPortfolioNavigation.PortfolioName,
                BenchmarkId = b.BenchmarkPortfolioNavigation.PortfolioId,
                BenchmarkName = b.BenchmarkPortfolioNavigation.PortfolioName
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

        private static PortfolioCumulativeDayPerformanceDTO MapToPortfolioCumulativeDayPerformanceDTO(PortfolioCumulativeDayPerformance p)
            => new PortfolioCumulativeDayPerformanceDTO
            {
                Bankday = p.Bankday,
                Value = p!.CumulativeDayPerformance!.Value
            };

        public async Task<List<PortfolioCumulativeDayPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync()
            => (await _portfolioRepository.GetAllPortfoliosAsync())
                    .SelectMany(p => p.PortfolioCumulativeDayPerformancesNavigation)
                    .Select(MapToPortfolioCumulativeDayPerformanceDTO)
                    .ToList();

        public async Task<List<PortfolioCumulativeDayPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId)
            => (await _portfolioRepository.GetPortfolioAsync(portfolioId))
                    .PortfolioCumulativeDayPerformancesNavigation
                    .Select(MapToPortfolioCumulativeDayPerformanceDTO)
                    .ToList();

        public async Task<int> GetBenchmarkId(int portfolioId)
            => (await _benchmarkRepository.GetBenchmarkMappingsAsync())
                .Single(b => b.PortfolioId == portfolioId)
                .BenchmarkId;

        private static PortfolioBenchmarkCumulativeDayPerformanceDTO MapToPortfolioBenchmarkCumulativeDayPerformanceDTO(
            PortfolioCumulativeDayPerformanceDTO portfolioPerformance,
            PortfolioCumulativeDayPerformanceDTO benchmarkPerformance
        )
            => new PortfolioBenchmarkCumulativeDayPerformanceDTO
            {
                Bankday = portfolioPerformance.Bankday,
                PortfolioValue = portfolioPerformance.Value,
                BenchmarkValue = benchmarkPerformance.Value
            };

        public async Task<List<PortfolioBenchmarkCumulativeDayPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var benchmarkId = await GetBenchmarkId(portfolioId);

            var portfolioPerformance = await GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);
            var benchmarkPerformance = await GetPortfolioCumulativeDayPerformanceDTOsAsync(benchmarkId);

            return portfolioPerformance
                .Join(
                    benchmarkPerformance,
                    pp => pp.Bankday,
                    bp => bp.Bankday,
                    MapToPortfolioBenchmarkCumulativeDayPerformanceDTO
                )
                .OrderBy(dto => dto.Bankday)
                .ToList();
        }

    }

}