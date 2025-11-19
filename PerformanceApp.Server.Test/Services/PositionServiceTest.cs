using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using Moq;

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

    private static PositionValue CreatePositionValue(DateOnly bankday, decimal value)
    {
        return new PositionValue { Bankday = bankday, Value = value };
    }
    private static InstrumentPrice CreateInstrumentPrice(DateOnly bankday, decimal price)
    {
        return new InstrumentPrice { Bankday = bankday, Price = price };
    }
    private static Instrument CreateInstrument(string name, DateOnly bankday, decimal price)
    {
        return new Instrument { Name = name, InstrumentPricesNavigation = [CreateInstrumentPrice(bankday, price)] };
    }
    private static Position CreatePosition(int portfolioId, int instrumentId, DateOnly bankday, int? count = null, decimal? nominal = null, decimal? proportion = null, decimal? positionValue = null, string? instrumentName = null, decimal? instrumentPrice = null)
    {
        return new Position
        {
            PortfolioId = portfolioId,
            InstrumentId = instrumentId,
            Bankday = bankday,
            Count = count,
            Nominal = nominal,
            Proportion = proportion,
            InstrumentNavigation = instrumentName != null && instrumentPrice != null
                ? CreateInstrument(instrumentName, bankday, instrumentPrice.Value)
                : null,
            PositionValuesNavigation = positionValue != null
                ? [CreatePositionValue(bankday, positionValue.Value)]
                : []
        };
    }
    private static Position CreateStockPosition(int portfolioId, int instrumentId, DateOnly bankday, int count, decimal? positionValue = null, string? instrumentName = null, decimal? instrumentPrice = null)
    {
        return CreatePosition(portfolioId, instrumentId, bankday, count: count, positionValue: positionValue, instrumentName: instrumentName, instrumentPrice: instrumentPrice);
    }
    private static Position CreateBondPosition(int portfolioId, int instrumentId, DateOnly bankday, decimal nominal, decimal? positionValue = null, string? instrumentName = null, decimal? instrumentPrice = null)
    {
        return CreatePosition(portfolioId, instrumentId, bankday, nominal: nominal, positionValue: positionValue, instrumentName: instrumentName, instrumentPrice: instrumentPrice);
    }
    private static Position CreateIndexPosition(int portfolioId, int instrumentId, DateOnly bankday, decimal proportion, decimal? positionValue = null, string? instrumentName = null, decimal? instrumentPrice = null)
    {
        return CreatePosition(portfolioId, instrumentId, bankday, proportion: proportion, positionValue: positionValue, instrumentName: instrumentName, instrumentPrice: instrumentPrice);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ReturnsMappedStockPositionDTOs()
    {
        // Arrange
        var bankday = new DateOnly(2024, 1, 1);
        int portfolioId = 1;

        var positions = new List<Position>
        {
            CreateStockPosition(
                portfolioId: portfolioId,
                instrumentId: 100,
                bankday: bankday,
                count: 50,
                positionValue: 10000.0m,
                instrumentName: "Test Stock",
                instrumentPrice: 200.0m
            )
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
            CreateBondPosition(
                portfolioId: portfolioId,
                instrumentId: 200,
                bankday: bankday,
                nominal: 1000.0m,
                positionValue: 1050.0m,
                instrumentName: "Test Bond",
                instrumentPrice: 105.0m
            )
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
            CreateIndexPosition(
                portfolioId: portfolioId,
                instrumentId: 300,
                bankday: bankday,
                proportion: 0.25m,
                positionValue: 375.0m,
                instrumentName: "Test Index",
                instrumentPrice: 1500.0m
            )
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