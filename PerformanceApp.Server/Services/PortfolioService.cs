using PerformanceApp.Server.Models;
using PerformanceApp.Server.Repositories;

namespace PerformanceApp.Server.Services
{
    public interface IPortfolioService
    {
        Task<List<PortfolioDTO>> GetPortfolioDTOsAsync();
    }

    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _repo;

        public PortfolioService(IPortfolioRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PortfolioDTO>> GetPortfolioDTOsAsync()
        {
            var portfolios = await _repo.GetPortfoliosAsync();

            return portfolios
                .Select(
                    p => new PortfolioDTO
                    {
                        PortfolioId = p.PortfolioId,
                        PortfolioName = p.PortfolioName
                    }
                ).ToList();
        }

    }

}