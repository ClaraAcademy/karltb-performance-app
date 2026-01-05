using Moq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Test.Svg.Extractors;

public class YExtractorTest
{
    [Fact]
    public void ExtractY1s_ProducesCorrectValues()
    {
        // Arrange
        var n = 17;
        var points = new List<DataPoint2>();

        for (int i = 0; i < n; i++)
        {
            var date = new DateOnly(2025, 1, i + 1);
            var y1 = i * 10f;
            var y2 = 0;
            points.Add(new DataPoint2(date, y1, y2));
        }

        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<float>()))
            .Returns<float>(y => y);

        var extractor = new YExtractor(mockScaler.Object);

        // Act
        var result = extractor.ExtractY1s(points);

        // Assert
        Assert.Equal(n, result.Count);
        for (int i = 0; i < n; i++)
        {
            Assert.Equal(i * 10f, result[i]);
        }
    }

    [Fact]
    public void ExtractY1s_HandlesEmptyInput()
    {
        // Arrange
        var points = new List<DataPoint2>();

        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<int>()))
            .Returns<int>(_ => 7);

        var extractor = new YExtractor(mockScaler.Object);

        // Act
        var result = extractor.ExtractY1s(points);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ExtractY2s_ProducesCorrectValues()
    {
        // Arrange
        var n = 15;
        var points = new List<DataPoint2>();

        for (int i = 0; i < n; i++)
        {
            var date = new DateOnly(2025, 1, i + 1);
            var y1 = 0;
            var y2 = i * 100f;
            points.Add(new DataPoint2(date, y1, y2));
        }

        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<float>()))
            .Returns<float>(y => y);

        var extractor = new YExtractor(mockScaler.Object);

        // Act
        var result = extractor.ExtractY2s(points);

        // Assert
        Assert.Equal(n, result.Count);
        for (int i = 0; i < n; i++)
        {
            Assert.Equal(i * 100f, result[i]);
        }
    }

    [Fact]
    public void ExtractY2s_HandlesEmptyInput()
    {
        // Arrange
        var points = new List<DataPoint2>();

        var mockScaler = new Mock<IScaler>();
        mockScaler
            .Setup(s => s.Scale(It.IsAny<int>()))
            .Returns<int>(_ => 7);

        var extractor = new YExtractor(mockScaler.Object);

        // Act
        var result = extractor.ExtractY2s(points);

        // Assert
        Assert.Empty(result);
    }
}