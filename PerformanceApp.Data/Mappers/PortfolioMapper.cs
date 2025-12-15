using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Mappers;

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
    public static List<PortfolioBenchmarkDTO> MapToPortfolioBenchmarkDTOs(IEnumerable<Portfolio> portfolios)
    {
        return portfolios
            .SelectMany(MapToPortfolioBenchmarkDTOs)
            .ToList();
    }

    private static PortfolioDTO MapToPortfolioDTO(int id, string name)
    {
        return new PortfolioDTO { PortfolioId = id, PortfolioName = name };
    }

    public static PortfolioBenchmarkKeyFigureDTO MapToPortfolioBenchmarkKeyFigureDTO(Portfolio portfolio, Portfolio Benchmark, KeyFigureValue portfolioKfv)
    {
        var keyFigureId = portfolioKfv.KeyFigureInfoNavigation.Id;
        var keyFigureName = portfolioKfv.KeyFigureInfoNavigation.Name;

        var portfolioValue = portfolio
            .KeyFigureValuesNavigation
            .SingleOrDefault(kfv => kfv.KeyFigureId == keyFigureId)?.Value;

        var benchmarkValue = Benchmark
            .KeyFigureValuesNavigation
            .SingleOrDefault(kfv => kfv.KeyFigureId == keyFigureId)?.Value;

        return new PortfolioBenchmarkKeyFigureDTO
        {
            KeyFigureId = keyFigureId,
            KeyFigureName = keyFigureName,
            PortfolioId = portfolio.Id,
            PortfolioName = portfolio.Name,
            PortfolioValue = portfolioValue,
            BenchmarkId = Benchmark.Id,
            BenchmarkName = Benchmark.Name,
            BenchmarkValue = benchmarkValue
        };
    }

}