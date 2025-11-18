using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;
using PerformanceApp.Server.Services;
using Moq;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Server.Test.Services;

public class PositionServiceTest
{
    private readonly Mock<IPositionRepository> _positionRepositoryMock;
    private readonly PositionService _positionService;

    public PositionServiceTest()
    {
        _positionRepositoryMock = new Mock<IPositionRepository>();
        _positionService = new PositionService(_positionRepositoryMock.Object);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsMappedStockPositionDTOs()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        var positions = new List<Position>
        {
            new Position
            {
                PortfolioId = portfolioId,
                InstrumentId = 100,
                Bankday = bankday,
                Count = 50,
                InstrumentNavigation = new Instrument
                {
                    Name = "Test Stock",
                    InstrumentPricesNavigation = new List<InstrumentPrice>
                    {
                        new InstrumentPrice { Bankday = bankday, Price = 200.0m }
                    }
                },
                PositionValuesNavigation = new List<PositionValue>
                {
                    new PositionValue { Bankday = bankday, Value = 10000.0m }
                }
            }
        };

        _positionRepositoryMock.Setup(r => r.GetStockPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(positions);

        // Act
        var result = await _positionService.GetStockPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Single(result);
        var dto = result[0];
        Assert.Equal(portfolioId, dto.PortfolioId);
        Assert.Equal(100, dto.InstrumentId);
        Assert.Equal("Test Stock", dto.InstrumentName);
        Assert.Equal(bankday, dto.Bankday);
        Assert.Equal(10000.0m, dto.Value);
        Assert.Equal(200.0m, dto.UnitPrice);
        Assert.Equal(50, dto.Count);
    }

    [Fact]
    public async Task GetStockPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetStockPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetStockPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_NoStockPositions_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetStockPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetStockPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_InvalidPortfolioId_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = -1;

        _positionRepositoryMock.Setup(r => r.GetStockPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetStockPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetStockPositionsAsync_InvalidBankday_ReturnsEmptyList()
    {
        // Arrange
        var bankday = DateOnly.MinValue;
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetStockPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetStockPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_ReturnsMappedBondPositionDTOs()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        var positions = new List<Position>
        {
            new Position
            {
                PortfolioId = portfolioId,
                InstrumentId = 200,
                Bankday = bankday,
                Nominal = 1000.0m,
                InstrumentNavigation = new Instrument
                {
                    Name = "Test Bond",
                    InstrumentPricesNavigation = new List<InstrumentPrice>
                    {
                        new InstrumentPrice { Bankday = bankday, Price = 105.0m }
                    }
                },
                PositionValuesNavigation = new List<PositionValue>
                {
                    new PositionValue { Bankday = bankday, Value = 1050.0m }
                }
            }
        };

        _positionRepositoryMock.Setup(r => r.GetBondPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(positions);

        // Act
        var result = await _positionService.GetBondPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Single(result);
        var dto = result[0];
        Assert.Equal(portfolioId, dto.PortfolioId);
        Assert.Equal(200, dto.InstrumentId);
        Assert.Equal("Test Bond", dto.InstrumentName);
        Assert.Equal(bankday, dto.Bankday);
        Assert.Equal(1050.0m, dto.Value);
        Assert.Equal(105.0m, dto.UnitPrice);
        Assert.Equal(1000.0m, dto.Nominal);
    }

    [Fact]
    public async Task GetBondPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetBondPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetBondPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_InvalidPortfolioId_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = -1;

        _positionRepositoryMock.Setup(r => r.GetBondPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetBondPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBondPositionsAsync_InvalidBankday_ReturnsEmptyList()
    {
        // Arrange
        var bankday = DateOnly.MinValue;
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetBondPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetBondPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsMappedIndexPositionDTOs()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        var positions = new List<Position>
        {
            new Position
            {
                PortfolioId = portfolioId,
                InstrumentId = 300,
                Bankday = bankday,
                Proportion = 0.25m,
                InstrumentNavigation = new Instrument
                {
                    Name = "Test Index",
                    InstrumentPricesNavigation = new List<InstrumentPrice>
                    {
                        new InstrumentPrice { Bankday = bankday, Price = 1500.0m }
                    }
                },
                PositionValuesNavigation = new List<PositionValue>
                {
                    new PositionValue { Bankday = bankday, Value = 375.0m }
                }
            }
        };

        _positionRepositoryMock.Setup(r => r.GetIndexPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(positions);

        // Act
        var result = await _positionService.GetIndexPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Single(result);
        var dto = result[0];
        Assert.Equal(portfolioId, dto.PortfolioId);
        Assert.Equal(300, dto.InstrumentId);
        Assert.Equal("Test Index", dto.InstrumentName);
        Assert.Equal(bankday, dto.Bankday);
        Assert.Equal(375.0m, dto.Value);
        Assert.Equal(1500.0m, dto.UnitPrice);
        Assert.Equal(0.25m, dto.Proportion);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetIndexPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetIndexPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_InvalidPortfolioId_ReturnsEmptyList()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = -1;

        _positionRepositoryMock.Setup(r => r.GetIndexPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetIndexPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetIndexPositionsAsync_InvalidBankday_ReturnsEmptyList()
    {
        // Arrange
        var bankday = DateOnly.MinValue;
        int portfolioId = 1;

        _positionRepositoryMock.Setup(r => r.GetIndexPositionsAsync(bankday, portfolioId))
            .ReturnsAsync(new List<Position>());

        // Act
        var result = await _positionService.GetIndexPositionsAsync(bankday, portfolioId);

        // Assert
        Assert.Empty(result);
    }

}