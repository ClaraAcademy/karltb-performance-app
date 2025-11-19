namespace PerformanceApp.Server.Dtos;

public partial class PortfolioPerformanceDTO
{
    public DateOnly Bankday { get; set; }

    public decimal Value { get; set; }
}
