using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services.Mappers;

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

    public async Task<string> GetLineChart(int portfolioId, int? width = null, int? height = null)
    {
        var dtos = await _portfolioService.GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        if (dtos == null || dtos.Count == 0)
        {
            return string.Empty;
        }

        var dataPoints = PortfolioPerformanceMapper.MapToDataPoint2s(dtos);

        return new SVG(
            dataPoints,
            width ?? DefaultWidth,
            height ?? DefaultHeight
        ).ToString();
    }
}

