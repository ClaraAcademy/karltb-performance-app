using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IPortfolioService
{
    Task<int> GetPortfolioIdAsync(string name);
}

public class PortfolioService : IPortfolioService
{
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioService(PadbContext context)
    {
        _portfolioRepository = new PortfolioRepository(context);
    }

    public async Task<int> GetPortfolioIdAsync(string name)
    {
        var portfolios = await _portfolioRepository.GetPortfoliosAsync();

        var portfolio = portfolios.FirstOrDefault(p => p.Name == name)
            ?? throw new KeyNotFoundException($"Portfolio with name '{name}' not found.");

        return portfolio.Id;
    }

}