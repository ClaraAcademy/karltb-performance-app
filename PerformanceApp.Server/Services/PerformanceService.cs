using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Mappers;
namespace PerformanceApp.Server.Services
{
    public interface IPerformanceService
    {
        Task<List<PortfolioBenchmarkKeyFigureDTO>> GetPortfolioBenchmarkKeyFigureValues(int portfolioId);
    }

    public class PerformanceService(IPortfolioRepository portfolioRepository)
        : IPerformanceService
    {
        private readonly IPortfolioRepository _portfolioRepository = portfolioRepository;

        public async Task<List<PortfolioBenchmarkKeyFigureDTO>> GetPortfolioBenchmarkKeyFigureValues(int portfolioId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioAsync(portfolioId);

            if (portfolio == null)
            {
                return [];
            }

            var dtos =
                from benchmark in portfolio.BenchmarksNavigation
                from kfi in portfolio.KeyFigureValuesNavigation
                select PortfolioMapper.MapToPortfolioBenchmarkKeyFigureDTO(portfolio, benchmark, kfi);

            return dtos.ToList();
        }
    }
}