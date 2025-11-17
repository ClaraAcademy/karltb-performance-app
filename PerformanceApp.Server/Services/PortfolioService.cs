using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Server.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync();
        Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId);
        Task<List<PortfolioPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId);
        Task<List<PortfolioBenchmarkPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId);
    }

    public class PortfolioService(IPortfolioRepository portfolioRepository, IBenchmarkRepository benchmarkRepository) : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;
        private readonly IBenchmarkRepository _benchmarkRepository = benchmarkRepository;

        private static PortfolioDTO MapToPortfolioDTO(Portfolio p)
            => new PortfolioDTO { PortfolioId = p.Id, PortfolioName = p.Name };

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync()
            => (await _portfolioRepository.GetProperPortfoliosAsync())
                .Select(MapToPortfolioDTO)
                .ToList();

        private static PortfolioBenchmarkDTO MapToPortfolioBenchmarkDTO(Benchmark b)
            => new PortfolioBenchmarkDTO
            {
                PortfolioId = b.PortfolioPortfolioNavigation.Id,
                PortfolioName = b.PortfolioPortfolioNavigation.Name,
                BenchmarkId = b.BenchmarkPortfolioNavigation.Id,
                BenchmarkName = b.BenchmarkPortfolioNavigation.Name
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


        private static PortfolioPerformanceDTO MapToPortfolioPerformanceDTO(PortfolioPerformance portfolioPerformance)
        {
            return new PortfolioPerformanceDTO
            {
                Bankday = portfolioPerformance.PeriodStart,
                Value = portfolioPerformance.Value
            };
        }

        public async Task<List<PortfolioPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(portfolioId);

            return portfolio.PortfolioPerformancesNavigation
                .Select(MapToPortfolioPerformanceDTO)
                .ToList();
        }

        public async Task<int> GetBenchmarkId(int portfolioId)
            => (await _benchmarkRepository.GetBenchmarkMappingsAsync())
                .Single(b => b.PortfolioId == portfolioId)
                .BenchmarkId;

        private static PortfolioBenchmarkPerformanceDTO MapToPortfolioBenchmarkCumulativeDayPerformanceDTO(
            PortfolioPerformanceDTO portfolioPerformance,
            PortfolioPerformanceDTO benchmarkPerformance
        )
        {
            return new PortfolioBenchmarkPerformanceDTO
            {
                Bankday = portfolioPerformance.Bankday,
                PortfolioValue = portfolioPerformance.Value,
                BenchmarkValue = benchmarkPerformance.Value
            };
        }

        private static DateOnly GetBankday(PortfolioPerformanceDTO dto) => dto.Bankday;

        public async Task<List<PortfolioBenchmarkPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var benchmarkId = await GetBenchmarkId(portfolioId);

            var portfolioPerformance = await GetPortfolioCumulativeDayPerformanceDTOsAsync(portfolioId);
            var benchmarkPerformance = await GetPortfolioCumulativeDayPerformanceDTOsAsync(benchmarkId);

            return portfolioPerformance
                .Join(benchmarkPerformance, GetBankday, GetBankday, MapToPortfolioBenchmarkCumulativeDayPerformanceDTO)
                .OrderBy(dto => dto.Bankday)
                .ToList();
        }
    }

}