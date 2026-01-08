using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Formatters;

public class PercentageFormatterTest
{
    [Fact]
    public void Format_SingleValue_ReturnsFormattedString()
    {
        // Arrange
        var formatter = new PercentageFormatter();
        var value = 0.256f;

        // Act
        var result = formatter.Format(value);

        // Assert
        Assert.Equal("26 %", result);
    }

    [Fact]
    public void Format_MultipleValues_ReturnsFormattedStrings()
    {
        // Arrange
        var formatter = new PercentageFormatter();
        var values = new List<float> { 0.256f, 0.5f, 0.1234f };

        // Act
        var results = formatter.Format(values).ToList();

        // Assert
        Assert.Equal(["26 %", "50 %", "12 %"], results);
    }
}