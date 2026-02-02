using System.Globalization;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class PointFactoryTest
{
    static string ToPoint(float x, float y)
    {
        static string format(float f) => f.ToString("0", CultureInfo.InvariantCulture);
        return $"{format(x)},{format(y)}";
    }

    [Fact]
    public void Points_ReturnsCorrectlyFormattedPoints()
    {
        // Arrange
        var xs = new float[] { 1.5f, 2.25f, 3.75f };
        var ys = new float[] { 4.5f, 5.25f, 6.75f };
        var factory = new PointFactory(ToPoint, xs, ys);

        // Act
        var points = factory.Points.ToList();

        // Assert
        Assert.Equal(3, points.Count);
        Assert.Equal("2,5", points[0]);
        Assert.Equal("2,5", points[1]);
        Assert.Equal("4,7", points[2]);
    }

    [Fact]
    public void Points_HandlesDifferentLengthEnumerables()
    {
        // Arrange
        var xs = new float[] { 1f, 2f };
        var ys = new float[] { 3f };
        var factory = new PointFactory(ToPoint, xs, ys);

        // Act
        var points = factory.Points.ToList();

        // Assert
        Assert.Single(points);
        Assert.Equal("1,3", points[0]);
    }

    [Fact]
    public void Points_EmptyEnumerables_ReturnsEmpty()
    {
        // Arrange
        var factory = new PointFactory(ToPoint, [], []);

        // Act
        var points = factory.Points.ToList();

        // Assert
        Assert.Empty(points);
    }
}