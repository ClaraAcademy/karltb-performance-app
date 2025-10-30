namespace PerformanceApp.Server.Models;

public class PortfolioBenchmarkDTO
{
    public int PortfolioID { get; set; }
    public string PortfolioName { get; set; } = null!;
    public int BenchmarkID { get; set; }
    public string BenchmarkName { get; set; } = null!;
}