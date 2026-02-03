using PerformanceApp.Data.Svg.Scalers.Index;

namespace PerformanceApp.Data.Test.Svg.Scalers.Index;

public class IndexScalerTest
{
    [Theory]
    [InlineData(100, 10, 10, 10, 0)]
    [InlineData(100, 10, 10, 54, 5)]
    [InlineData(100, 10, 10, 100 - 10, 9)]
    public void Scale_ReturnsExpectedValue(int length, int margin, int total, float expected, int index)
    {
        // Arrange
        var scaler = new IndexScaler(length, margin, total);

        // Act
        var result = scaler.Scale(index);

        // Assert
        Assert.Equal(expected, result, 0);
    }

    [Fact]
    public void Constructor_SetsCorrectScaleAndOffset()
    {
        // Arrange
        int length = 200;
        int margin = 20;
        int total = 5;

        // Act
        var scaler = new IndexScaler(length, margin, total);

        // Assert
        float expectedScale = (length - 2f * margin) / (total - 1f);
        Assert.Equal(margin, scaler.Offset);
        Assert.Equal(expectedScale, scaler.Step);
    }
}