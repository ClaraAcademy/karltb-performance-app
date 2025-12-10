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

    public static List<PortfolioBenchmarkDTO> MapToPortfolioBenchmarkDTOs(Portfolio portfolio)
    {
        return portfolio.BenchmarkPortfoliosNavigation
            .Select(BenchmarkMapper.MapToPortfolioBenchmarkDTO)
            .ToList();
    }

    private static PortfolioDTO MapToPortfolioDTO(int id, string name)
    {
        return new PortfolioDTO { PortfolioId = id, PortfolioName = name };
    }

}