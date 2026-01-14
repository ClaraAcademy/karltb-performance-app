using PerformanceApp.Data.Svg.Samplers.Coordinate;

namespace PerformanceApp.Data.Test.Svg.Samplers.Coordinate;

public class XCoordinateSamplerTest
{
    [Fact]
    public void Transform_ShouldReturnExpectedCoordinate()
    {
        // Arrange
        var count = 5;
        var margin = 10;
        var width = 110;
        var index = 7;
        var sampler = new XCoordinateSampler(count, margin, width);
        var expected = margin + index * (width - 2f * margin) / (count - 1f);

        // Act
        var actual = sampler.Transform(index);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sample_ShouldReturnExpectedCoordinates()
    {
        // Arrange
        var count = 190;
        var margin = 10;
        var width = 510;
        var sampler = new XCoordinateSampler(count, margin, width);

        // Act
        var result = sampler.Sample();

        // Assert
        Assert.Equal(count, result.Count);
        for (int i = 0; i < count; i++)
        {
            var expected = margin + i * (width - 2f * margin) / (count - 1f);
            Assert.Equal(expected, result[i], 2);
        }
    }


}