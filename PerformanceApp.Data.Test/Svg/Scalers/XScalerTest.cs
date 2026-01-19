using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Test.Svg.Scalers;

public class XScalerTest
{
    private const int Width = 200;
    private const int Margin = 10;
    private const int NumberOfPoints = 50;

    [Theory]
    [InlineData(0, Margin)] // m + 0 * step = m
    [InlineData(NumberOfPoints, Width - Margin)] // m + n * (w - 2m) / n = w - m
    [InlineData(NumberOfPoints / 2, Width / 2)] // m + (n / 2) * (w - 2m) / n = w/2
    public void Scale_ShouldReturnCorrectScaledValue(float input, float expected)
    {
        // Arrange
        var width = Width;
        var margin = Margin;
        var numberOfPoints = NumberOfPoints;
        var scaler = new XScaler(width, margin, numberOfPoints);

        // Act
        var scaledValue = scaler.Scale(input);

        // Assert
        Assert.Equal(expected, scaledValue, 2);
    }

}