using PerformanceApp.Data.Models;
using PerformanceApp.Seeder.Constants;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Mappers;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Infrastructure.Repositories;

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
        Task<List<PortfolioBenchmarkPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId);
    }

    public class PortfolioService(IPortfolioRepository portfolioRepository) : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

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

            if (portfolios == null || !portfolios.Any())
            {
                return [];
            }

            return PortfolioMapper.MapToPortfolioBenchmarkDTOs(portfolios);
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync()
        {
            var portfolios = await _portfolioRepository.GetProperPortfoliosAsync();

            if (portfolios == null || !portfolios.Any())
            {
                return [];
            }

            return PortfolioMapper.MapToPortfolioBenchmarkDTOs(portfolios);
        }

        public async Task<List<PortfolioBenchmarkDTO>> GetPortfolioBenchmarksAsync(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(portfolioId);

            if (portfolio == null)
            {
                return [];
            }

            var dtos = PortfolioMapper
                .MapToPortfolioBenchmarkDTOs(portfolio)
                .ToList();

            return dtos;
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

        public async Task<List<PortfolioBenchmarkPerformanceDTO>> GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(portfolioId);
            if (portfolio == null)
            {
                return [];
            }

            var benchmark = portfolio.BenchmarksNavigation.SingleOrDefault();
            if (benchmark == null)
            {
                return [];
            }

            var portfolioPerformances = portfolio.GetCumulativeDayPerformanceDtos();
            var benchmarkPerformances = benchmark.GetCumulativeDayPerformanceDtos();
            var empty = !portfolioPerformances.Any() && !benchmarkPerformances.Any();
            if (empty)
            {
                return [];
            }

            return PortfolioPerformanceHelper.Join(portfolioPerformances, benchmarkPerformances);
        }
    }

}