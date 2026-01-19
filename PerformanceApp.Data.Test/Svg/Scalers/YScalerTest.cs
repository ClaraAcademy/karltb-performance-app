using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Test.Svg.Scalers;

public class YScalerTest
{
    private const int Height = 200;
    private const int Margin = 20;
    private const float Max = 100f;
    private const float Min = 1f;

    [Theory]
    [InlineData(Max, Margin)] // m + (max - max) * (h - 2m) / (max - min) = m 
    [InlineData(Min, Height - Margin)] // m + (max - min) * (h - 2m) / (max - min) = h - 2m
    public void Scale_ShouldReturnCorrectScaledValue(float input, float expected)
    {
        // Arrange
        var height = Height;
        var margin = Margin;
        var max = Max;
        var min = Min;
        var scaler = new YScaler(height, margin, max, min);

        // Act
        var scaledValue = scaler.Scale(input);

        // Assert
        Assert.Equal(expected, scaledValue, 2);
    }


}