using PerformanceApp.Data.Svg.Scalers.Value;

namespace PerformanceApp.Data.Test.Svg.Scalers.Value;

public class ValueScalerTest
{
    [Theory]
    [InlineData(100, 10, 50f, 0f, false, 0f, 10f)]   // Min value, not inverted
    [InlineData(100, 10, 50f, 0f, false, 25f, 50f)]  // Mid value, not inverted
    [InlineData(100, 10, 50f, 0f, false, 50f, 100f - 10f)] // Max value, not inverted
    public void Scale_ReturnsExpectedValue_NotInverted(int length, int margin, float max, float min, bool inverted, float value, float expected)
    {
        // Arrange 
        var scaler = new ValueScaler(length, margin, max, min, inverted);
        // Act
        var result = scaler.Scale(value);
        // Assert
        Assert.Equal(expected, result, 2);
    }

    [Theory]
    [InlineData(100, 10, 50f, 0f, true, 0f, 100f - 10f)]   // Min value, inverted
    [InlineData(100, 10, 50f, 0f, true, 25f, 50f)]         // Mid value, inverted
    [InlineData(100, 10, 50f, 0f, true, 50f, 10f)]         // Max value, inverted
    public void Scale_ReturnsExpectedValue_Inverted(int length, int margin, float max, float min, bool inverted, float value, float expected)
    {
        // Arrange
        var scaler = new ValueScaler(length, margin, max, min, inverted);
        // Act
        var result = scaler.Scale(value);
        // Assert
        Assert.Equal(expected, result, 2);
    }

    [Fact]
    public void Scale_HandlesNegativeValues()
    {
        // Arrange
        var scaler = new ValueScaler(100, 10, 0f, -50f, false);
        // Act
        var result = scaler.Scale(-25f);
        // Assert
        Assert.Equal(-30, result, 2);
    }

    [Fact]
    public void Scale_HandlesZeroLength()
    {
        var scaler = new ValueScaler(0, 0, 1f, 0f, false);
        var result = scaler.Scale(0.5f);
        Assert.Equal(0f, result, 2);
    }
}