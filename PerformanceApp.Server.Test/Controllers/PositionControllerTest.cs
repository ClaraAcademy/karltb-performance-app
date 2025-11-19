using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PerformanceApp.Server.Controllers;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Controllers;

public class PositionControllerTest : ControllerTestBase
{
    private readonly Mock<IPositionService> _mockPositionService;

    public PositionControllerTest()
    {
        _mockPositionService = new Mock<IPositionService>();
    }

    private static StockPositionDTO GetStockPositionDTO(int id)
    {
        return new StockPositionDTO
        {
            PortfolioId = id,
            InstrumentId = id + Random.Shared.Next(1000, 2000),
            InstrumentName = $"Stock {id}",
            Bankday = DateOnly.Parse("2025-10-31"),
            Value = id * 10 + 0.5m,
            UnitPrice = id * 2 + 0.25m,
        };
    }

    private static BondPositionDTO GetBondPositionDTO(int id)
    {
        return new BondPositionDTO
        {
            PortfolioId = id,
            InstrumentId = id + 2000,
            InstrumentName = $"Bond {id}",
            Bankday = DateOnly.Parse("2025-10-31"),
            Value = id * 20 + 0.75m,
            UnitPrice = id * 3 + 0.5m,
            Nominal = id * 1000 + 50m
        };
    }

    private static IndexPositionDTO GetIndexPositionDTO(int id)
    {
        return new IndexPositionDTO
        {
            PortfolioId = id,
            InstrumentId = id + 3000,
            InstrumentName = $"Index {id}",
            Bankday = DateOnly.Parse("2025-10-31"),
            Value = id * 30 + 1.0m,
            UnitPrice = id * 4 + 0.75m,
            Proportion = id * 5 % 100 + 0.1m
        };
    }

    private static List<StockPositionDTO> CreateStockPositions(int id, int count)
    {
        return Enumerable.Range(1, count)
            .Select(_ => GetStockPositionDTO(id))
            .ToList();
    }

    private static List<BondPositionDTO> CreateBondPositions(int id, int count)
    {
        return Enumerable.Range(1, count)
            .Select(_ => GetBondPositionDTO(id))
            .ToList();
    }

    private static List<IndexPositionDTO> CreateIndexPositions(int id, int count)
    {
        return Enumerable.Range(1, count)
            .Select(_ => GetIndexPositionDTO(id))
            .ToList();
    }

    [Fact]
    public async Task GetStockPositions_ReturnsOk_WhenDataExists()
    {
        // Arrange
        int portfolioId = 123;
        var date = DateOnly.Parse("2025-10-31");
        var stockPositions = CreateStockPositions(portfolioId, 3);

        _mockPositionService.Setup(s => s.GetStockPositionsAsync(date, portfolioId))
            .ReturnsAsync(stockPositions);

        var controller = new PositionController(_mockPositionService.Object);

        // Act
        var result = await controller.GetStockPositions(portfolioId, date.ToString());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnedPositions = Assert.IsType<List<StockPositionDTO>>(okResult.Value);
        Assert.Equal(stockPositions.Count, returnedPositions.Count);
    }

    [Fact]
    public async Task GetStockPositions_ReturnsOk_WhenNoDataExists()
    {
        // Arrange
        int portfolioId = 123;
        var date = DateOnly.Parse("2025-10-31");

        _mockPositionService.Setup(s => s.GetStockPositionsAsync(date, portfolioId))
            .ReturnsAsync([]);

        var controller = new PositionController(_mockPositionService.Object);

        // Act
        var result = await controller.GetStockPositions(portfolioId, date.ToString());

        // Assert
        var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetBondPositions_ReturnsOk_WhenDataExists()
    {
        // Arrange
        int portfolioId = 123;
        var date = DateOnly.Parse("2025-10-31");
        var bondPositions = CreateBondPositions(portfolioId, 2);

        _mockPositionService.Setup(s => s.GetBondPositionsAsync(date, portfolioId))
            .ReturnsAsync(bondPositions);

        var controller = new PositionController(_mockPositionService.Object);

        // Act
        var result = await controller.GetBondPositions(portfolioId, date.ToString());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnedPositions = Assert.IsType<List<BondPositionDTO>>(okResult.Value);
        Assert.Equal(bondPositions.Count, returnedPositions.Count);
    }

    [Fact]
    public async Task GetBondPositions_ReturnsOk_WhenNoDataExists()
    {
        // Arrange
        int portfolioId = 123;
        var date = DateOnly.Parse("2025-10-31");

        _mockPositionService.Setup(s => s.GetBondPositionsAsync(date, portfolioId))
            .ReturnsAsync([]);

        var controller = new PositionController(_mockPositionService.Object);

        // Act
        var result = await controller.GetBondPositions(portfolioId, date.ToString());

        // Assert
        var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, notFoundResult.StatusCode);
    }

    [Fact]
    public async Task GetIndexPositions_ReturnsOk_WhenDataExists()
    {
        // Arrange
        int portfolioId = 123;
        var date = DateOnly.Parse("2025-10-31");
        var indexPositions = CreateIndexPositions(portfolioId, 4);

        _mockPositionService.Setup(s => s.GetIndexPositionsAsync(date, portfolioId))
            .ReturnsAsync(indexPositions);

        var controller = new PositionController(_mockPositionService.Object);

        // Act
        var result = await controller.GetIndexPositions(portfolioId, date.ToString());

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, okResult.StatusCode);
        var returnedPositions = Assert.IsType<List<IndexPositionDTO>>(okResult.Value);
        Assert.Equal(indexPositions.Count, returnedPositions.Count);
    }

    [Fact]
    public async Task GetIndexPositions_ReturnsNotFound_WhenNoDataExists()
    {
        // Arrange
        int portfolioId = 123;
        var date = DateOnly.Parse("2025-10-31");

        _mockPositionService.Setup(s => s.GetIndexPositionsAsync(date, portfolioId))
            .ReturnsAsync([]);

        var controller = new PositionController(_mockPositionService.Object);

        // Act
        var result = await controller.GetIndexPositions(portfolioId, date.ToString());

        // Assert
        var notFoundResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.OK, notFoundResult.StatusCode);
    }

}