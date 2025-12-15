namespace PerformanceApp.Data.Dtos;

public partial class PortfolioBenchmarkPerformanceDTO
{
    public DateOnly Bankday { get; set; }

    public decimal PortfolioValue { get; set; }

    public decimal BenchmarkValue { get; set; }
}
