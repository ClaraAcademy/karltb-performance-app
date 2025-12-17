using Moq;
using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Server.Test.Services.PositionServiceTests.Fixture;

namespace PerformanceApp.Server.Test.Services.PositionServiceTests;

public class StockPositionTests() : PerformanceServiceTestFixture()
{
    [Fact]
    public async Task GetStockPositionsAsync_ReturnsMappedStockPositionDTOs()
    {
        // Arrange
        var expected = new StockPositionBuilder()
            .Many(8)
            .ToList();

        _positionRepositoryMock
            .Setup(r => r.GetStockPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _positionService.GetStockPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.NotEmpty(actual);
        Assert.Equal(expected.Count, actual.Count);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.InstrumentNavigation!.Name, a.InstrumentName);
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(PositionHelper.GetPositionValue(e), a.Value);
            Assert.Equal(PositionHelper.GetInstrumentUnitPrice(e), a.UnitPrice);
            Assert.Equal(e.Count, a.Count);
        }
    }

    [Fact]
    public async Task GetStockPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        _positionRepositoryMock
            .Setup(r => r.GetStockPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync([]);

        // Act
        var result = await _positionService.GetStockPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Empty(result);
    }
}