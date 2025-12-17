
using Moq;
using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Helpers;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Test.Services.PositionServiceTests.Fixture;

namespace PerformanceApp.Server.Test.Services.PositionServiceTests;

public class IndexPositionTests : PerformanceServiceTestFixture
{
    [Fact]
    public async Task GetIndexPositionsAsync_ReturnsMappedIndexPositionDTOs()
    {
        // Arrange
        var expected = new IndexPositionBuilder()
            .Many(7)
            .ToList();

        _positionRepositoryMock
            .Setup(r => r.GetIndexPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync(expected);

        // Act
        var actual = await _positionService.GetIndexPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.NotEmpty(actual);
        foreach (var (e, a) in expected.Zip(actual))
        {
            Assert.Equal(e.PortfolioId, a.PortfolioId);
            Assert.Equal(e.InstrumentId, a.InstrumentId);
            Assert.Equal(e.InstrumentNavigation!.Name, a.InstrumentName);
            Assert.Equal(e.Bankday, a.Bankday);
            Assert.Equal(PositionHelper.GetPositionValue(e), a.Value);
            Assert.Equal(PositionHelper.GetInstrumentUnitPrice(e), a.UnitPrice);
            Assert.Equal(e.Proportion, a.Proportion);

        }
    }

    [Fact]
    public async Task GetIndexPositionsAsync_NoPositions_ReturnsEmptyList()
    {
        // Arrange
        _positionRepositoryMock
            .Setup(r => r.GetIndexPositionsAsync(It.IsAny<DateOnly>(), It.IsAny<int>()))
            .ReturnsAsync([]);

        // Act
        var result = await _positionService.GetIndexPositionsAsync(new DateOnly(), 1);

        // Assert
        Assert.Empty(result);
    }
}