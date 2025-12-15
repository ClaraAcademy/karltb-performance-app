using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Seeder.Services;

public interface IPortfolioService
{
    Task<int> GetPortfolioIdAsync(string name);
}

public class PortfolioService(PadbContext context) : IPortfolioService
{
    private readonly IPortfolioRepository _portfolioRepository = new PortfolioRepository(context);

    public async Task<int> GetPortfolioIdAsync(string name)
    {
        var portfolios = await _portfolioRepository.GetPortfoliosAsync();

        var portfolio = portfolios.FirstOrDefault(p => p.Name == name)
            ?? throw new KeyNotFoundException($"Portfolio with name '{name}' not found.");

        return portfolio.Id;
    }

}