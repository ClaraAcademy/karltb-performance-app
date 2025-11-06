using PerformanceApp.Server.Models;
using PerformanceApp.Server.Controllers;
using System.Xml.Linq;
using System.Globalization;
using PerformanceApp.Server.Repositories;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace PerformanceApp.Server.Services;

public interface ISvgService
{
    Task<string> GetLineChart(int portfolioId, int? width, int? height, int? border);
}

public class SvgService(IPortfolioService portfolioService) : ISvgService
{
    private readonly IPortfolioService _portfolioService = portfolioService;
    private readonly int DefaultWidth = 800;
    private readonly int DefaultHeight = 500;
    private readonly int DefaultBorder = 30;

    private DataPoint2 MapToDataPoint2(PortfolioBenchmarkCumulativeDayPerformanceDTO dto)
        => new DataPoint2
        {
            x = dto.Bankday,
            y1 = (float)dto.PortfolioValue,
            y2 = (float)dto.BenchmarkValue
        };

    public async Task<string> GetLineChart(int portfolioId, int? width = null, int? height = null, int? border = null)
    {
        var dtos = await _portfolioService
            .GetPortfolioBenchmarkCumulativeDayPerformanceDTOsAsync(portfolioId);

        var dataPoints = dtos
            .Select(MapToDataPoint2)
            .ToList();

        return new SVG(
            dataPoints,
            width ?? DefaultWidth,
            height ?? DefaultHeight,
            border ?? DefaultBorder
        ).ToString();
    }
}

