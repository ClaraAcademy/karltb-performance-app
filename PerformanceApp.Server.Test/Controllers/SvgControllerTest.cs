using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Controllers;

public class SvgControllerTest : ControllerTestBase
{
    private readonly Mock<ISvgService> _mockSvgService;

    public SvgControllerTest()
    {
        _mockSvgService = new Mock<ISvgService>();
    }

    [Fact]
    public async Task GetCumulativePerformanceGraph_ReturnsSvgContent_WhenPortfolioExists()
    {
        // Arrange
        int portfolioId = 1;
        string svgContent = "<svg>...</svg>";

        _mockSvgService.Setup(s => s.GetLineChart(portfolioId, null, null))
            .ReturnsAsync(svgContent);

        var controller = new SvgController(_mockSvgService.Object);

        var user = CreateUserPrincipal("testuser");

        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetCumulativePerformanceGraph(portfolioId);

        // Assert
        var contentResult = Assert.IsType<ContentResult>(result.Result);
        Assert.Equal("image/svg+xml", contentResult.ContentType);
        Assert.Equal(svgContent, contentResult.Content);
    }

    [Fact]
    public async Task GetCumulativePerformanceGraph_ReturnsNotFound_WhenPortfolioDoesNotExist()
    {
        // Arrange
        int portfolioId = 999;

        _mockSvgService.Setup(s => s.GetLineChart(portfolioId, null, null))
            .ReturnsAsync("");

        var controller = new SvgController(_mockSvgService.Object);

        var user = CreateUserPrincipal("testuser");

        controller.ControllerContext = CreateControllerContext(user);

        // Act
        var result = await controller.GetCumulativePerformanceGraph(portfolioId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

}