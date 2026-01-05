using Moq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Test.Svg.Extractors;

public class XExtractorTest
{
    [Fact]
    public void Extract_ProducesCorrectValues()
    {
        // Arrange
        var n = 19;
        var points = new List<DataPoint2>();

        for (int i = 0; i < n; i++)
        {
            var date = new DateOnly(2025, 1, i + 1);
            var y1 = i * 10f;
            var y2 = i * 100f;
            points.Add(new DataPoint2(date, y1, y2));
        }

        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<int>()))
            .Returns<float>(x => x);

        var extractor = new XExtractor(mockScaler.Object);

        // Act
        var result = extractor.Extract(points);

        // Assert
        Assert.Equal(n, result.Count);
        for (int i = 0; i < n; i++)
        {
            Assert.Equal(i, result[i]);
        }
    }

    [Fact]
    public void Extract_HandlesEmptyInput()
    {
        // Arrange
        var points = new List<DataPoint2>();

        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<float>()))
            .Returns<float>(_ => 7f);

        var extractor = new XExtractor(mockScaler.Object);

        // Act
        var result = extractor.Extract(points);

        // Assert
        Assert.Empty(result);
    }
}