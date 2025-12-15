namespace PerformanceApp.Data.Dtos;

public class PortfolioBenchmarkKeyFigureDTO
{
    public required int KeyFigureId { get; set; }
    public required string KeyFigureName { get; set; }
    public required int PortfolioId { get; set; }
    public required string PortfolioName { get; set; }
    public required decimal? PortfolioValue { get; set; }
    public required int BenchmarkId { get; set; }
    public required string BenchmarkName { get; set; }
    public required decimal? BenchmarkValue { get; set; }

}