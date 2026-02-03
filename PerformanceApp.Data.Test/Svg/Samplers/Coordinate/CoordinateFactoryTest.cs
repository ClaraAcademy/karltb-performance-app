using PerformanceApp.Data.Svg.Samplers.Coordinate;

namespace PerformanceApp.Data.Test.Svg.Samplers.Coordinate;

public class CoordinateFactoryTest
{
    [Fact]
    public void Coordinates_ShouldReturnScaledValues_ForIntInput()
    {
        // Arrange
        var values = new List<int> { 1, 2, 3 };
        static float scale(int x) => x * 2.5f;
        var factory = new CoordinateFactory<int>(values, scale);

        // Act
        var coordinates = factory.Coordinates.ToList();

        // Assert
        Assert.Equal([2.5f, 5f, 7.5f], coordinates);
    }

    [Fact]
    public void Coordinates_ShouldReturnEmpty_ForEmptyInput()
    {
        // Arrange
        var values = new List<int>();
        static float scale(int x) => x * 1.0f;
        var factory = new CoordinateFactory<int>(values, scale);

        // Act
        var coordinates = factory.Coordinates.ToList();

        // Assert
        Assert.Empty(coordinates);
    }

    [Fact]
    public void Coordinates_ShouldWorkWithDifferentTypes()
    {
        // Arrange
        var values = new List<double> { 1.1, 2.2, 3.3 };
        static float scale(double x) => (float)(x + 1.0);
        var factory = new CoordinateFactory<double>(values, scale);

        // Act
        var coordinates = factory.Coordinates.ToList();

        // Assert
        Assert.Equal([2.1f, 3.2f, 4.3f], coordinates);
    }
}