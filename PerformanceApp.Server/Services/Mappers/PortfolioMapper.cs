using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Services.Mappers;

public static class PortfolioMapper
{
    public static List<PortfolioDTO> MapToPortfolioDtos(IEnumerable<Portfolio> portfolios)
    {
        return portfolios
            .Select(MapToPortfolioDTO)
            .ToList();
    }

    public static PortfolioDTO MapToPortfolioDTO(Portfolio portfolio)
    {
        var id = portfolio.Id;
        var name = portfolio.Name;

        return MapToPortfolioDTO(id, name);
    }

    public static PortfolioBenchmarkDTO MapToPortfolioBenchmarkDTO(Portfolio portfolio, Portfolio benchmark)
    {
        return new PortfolioBenchmarkDTO
        {
            PortfolioId = portfolio.Id,
            PortfolioName = portfolio.Name,
            BenchmarkId = benchmark.Id,
            BenchmarkName = benchmark.Name
        };
    }

    public static List<PortfolioBenchmarkDTO> MapToPortfolioBenchmarkDTOs(Portfolio portfolio)
    {
        return portfolio
            .BenchmarksNavigation
            .Select(benchmark => MapToPortfolioBenchmarkDTO(portfolio, benchmark))
            .ToList();
    }

    private static PortfolioDTO MapToPortfolioDTO(int id, string name)
    {
        return new PortfolioDTO { PortfolioId = id, PortfolioName = name };
    }

}