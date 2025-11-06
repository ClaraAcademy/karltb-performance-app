namespace PerformanceApp.Server.DTOs;

public class PortfolioBenchmarkDTO
{
    public int PortfolioId { get; set; }
    public string PortfolioName { get; set; } = null!;
    public int BenchmarkId { get; set; }
    public string BenchmarkName { get; set; } = null!;
}