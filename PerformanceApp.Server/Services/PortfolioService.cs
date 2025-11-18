using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

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

        private static PortfolioDTO MapToPortfolioDTO(Portfolio portfolio)
        {
            var id = portfolio.Id;
            var name = portfolio.Name;

            return new PortfolioDTO { PortfolioId = id, PortfolioName = name };
        }

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync()
        {
            var portfolios = await _portfolioRepository.GetProperPortfoliosAsync();

            return portfolios.Select(MapToPortfolioDTO).ToList();
        }

        private static PortfolioBenchmarkDTO MapToPortfolioBenchmarkDTO(Benchmark benchmarkMapping)
        {
            var portfolioId = benchmarkMapping.PortfolioPortfolioNavigation.Id;
            var portfolioName = benchmarkMapping.PortfolioPortfolioNavigation.Name;
            var benchmarkId = benchmarkMapping.BenchmarkPortfolioNavigation.Id;
            var benchmarkName = benchmarkMapping.BenchmarkPortfolioNavigation.Name;

            return new PortfolioBenchmarkDTO
            {
                PortfolioId = portfolioId,
                PortfolioName = portfolioName,
                BenchmarkId = benchmarkId,
                BenchmarkName = benchmarkName
            };
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync()
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();

            return benchmarkMappings.Select(MapToPortfolioBenchmarkDTO).ToList();
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId)
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();

            return benchmarkMappings.Where(b => b.PortfolioId == portfolioId)
                .Select(MapToPortfolioBenchmarkDTO)
                .ToList();
        }


        private static PortfolioPerformanceDTO MapToPortfolioPerformanceDTO(PortfolioPerformance portfolioPerformance)
        {
            var bankday = portfolioPerformance.PeriodStart;
            var value = portfolioPerformance.Value;

            return new PortfolioPerformanceDTO { Bankday = bankday, Value = value };
        }

        private static bool IsCumulativeDayPerformance(PortfolioPerformance portfolioPerformance)
        {
            var performanceTypeName = portfolioPerformance.PerformanceTypeNavigation.Name;

            return performanceTypeName == PerformanceTypeData.CumulativeDayPerformance;
        }

        public async Task<List<PortfolioPerformanceDTO>> GetPortfolioCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(portfolioId);

            if (portfolio == null)
            {
                return [];
            }

            return portfolio.PortfolioPerformancesNavigation
                .Where(IsCumulativeDayPerformance)
                .Select(MapToPortfolioPerformanceDTO)
                .ToList();
        }

        public async Task<int> GetBenchmarkId(int portfolioId)
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();

            return benchmarkMappings.Single(b => b.PortfolioId == portfolioId).BenchmarkId;
        }

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