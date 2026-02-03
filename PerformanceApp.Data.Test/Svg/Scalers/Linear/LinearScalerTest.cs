using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Test.Svg.Scalers.Linear;

public class LinearScalerTest
{
    [Theory]
    [InlineData(0f, 1f, 0f, 0f)]
    [InlineData(0f, 1f, 5f, 5f)]
    [InlineData(2f, 3f, 4f, 14f)]
    [InlineData(-1f, 2f, 3f, 5f)]
    [InlineData(10f, 0f, 7f, 10f)]
    public void Scale_FloatValue_ReturnsExpected(float offset, float step, float value, float expected)
    {
        var scaler = new LinearScaler(offset, step);
        var result = scaler.Scale(value);
        Assert.Equal(expected, result, 5);
    }

    [Theory]
    [InlineData(0f, 1f, 0, 0f)]
    [InlineData(0f, 1f, 5, 5f)]
    [InlineData(2f, 3f, 4, 14f)]
    [InlineData(-1f, 2f, 3, 5f)]
    [InlineData(10f, 0f, 7, 10f)]
    public void Scale_IntValue_ReturnsExpected(float offset, float step, int value, float expected)
    {
        var scaler = new LinearScaler(offset, step);
        var result = scaler.Scale(value);
        Assert.Equal(expected, result, 5);
    }
}