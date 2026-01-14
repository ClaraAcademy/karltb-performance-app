using PerformanceApp.Data.Svg.Samplers.Coordinate;

namespace PerformanceApp.Data.Test.Svg.Samplers.Coordinate;

public class YCoordinateSamplerTest
{
    [Fact]
    public void Transform_ShouldReturnExpectedCoordinate()
    {
        // Arrange
        var count = 5;
        var margin = 10;
        var height = 110;
        var index = 2;
        var sampler = new YCoordinateSampler(count, margin, height);
        var expected = margin + (count - 1 - index) * (height - 2f * margin) / (count - 1f);

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
        var height = 510;
        var sampler = new YCoordinateSampler(count, margin, height);

        // Act
        var result = sampler.Sample();

        // Assert
        Assert.Equal(count, result.Count);
        for (int i = 0; i < count; i++)
        {
            var expected = margin + (count - 1 - i) * (height - 2f * margin) / (count - 1f);
            Assert.Equal(expected, result[i], 2);
        }

    }

}