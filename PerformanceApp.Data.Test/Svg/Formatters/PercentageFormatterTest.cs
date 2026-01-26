using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Formatters;

public class PercentageFormatterTest
{
    [Fact]
    public void Format_SingleValue_ReturnsFormattedString()
    {
        // Arrange
        var value = 0.756f;
        var expected = "76 %";

        // Act
        var actual = PercentageFormatter.Format(value);

        // Assert
        Assert.Equal(expected, actual);
    }
}