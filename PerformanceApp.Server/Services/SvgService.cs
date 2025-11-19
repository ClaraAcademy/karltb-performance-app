using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Services;

public interface ISvgService
{
    Task<string> GetLineChart(int portfolioId, int? width, int? height);
}

public class SvgService(IPortfolioService portfolioService) : ISvgService
{
    private readonly IPortfolioService _portfolioService = portfolioService;
    private readonly int DefaultWidth = 800;
    private readonly int DefaultHeight = 500;

    private DataPoint2 MapToDataPoint2(PortfolioBenchmarkPerformanceDTO dto)
    {
        var x = dto.Bankday;
        var y1 = (float)dto.PortfolioValue;
        var y2 = (float)dto.BenchmarkValue;

        return new(x, y1, y2);
    }

    public async Task<string> GetLineChart(int portfolioId, int? width = null, int? height = null)
    {
        var dtos = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        if (dtos == null || dtos.Count == 0)
        {
            return string.Empty;
        }

        var dataPoints = dtos.Select(MapToDataPoint2).ToList();

        return new SVG(
            dataPoints,
            width ?? DefaultWidth,
            height ?? DefaultHeight
        ).ToString();
    }
}

