using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Formatters;

public class DecimalFormatterTest
{
    [Fact]
    public void Format_SingleValue_ReturnsFormattedString()
    {
        // Arrange
        var value = 12.3456f;
        var expected = "12.35";

        // Act
        var actual = DecimalFormatter.Format(value);

        // Assert
        Assert.Equal(expected, actual);
    }
}