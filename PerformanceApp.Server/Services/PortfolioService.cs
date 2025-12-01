using PerformanceApp.Server.Dtos;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Server.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync();
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync(string userId);
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

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync(string userId)
        {
            var portfolios = await _portfolioRepository.GetPortfoliosAsync(userId);
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

        public async Task<Benchmark?> GetBenchmarkAsync(int portfolioId)
        {
            var benchmarkMappings = await _benchmarkRepository.GetBenchmarkMappingsAsync();
            var benchmark = benchmarkMappings.SingleOrDefault(b => b.PortfolioId == portfolioId);

            return benchmark;
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
                .Join(benchmarkPerformance, GetBankday, GetBankday, MapToPortfolioBenchmarkCumulativeDayPerformanceDTO)
                .OrderBy(dto => dto.Bankday)
                .ToList();
        }
    }

}