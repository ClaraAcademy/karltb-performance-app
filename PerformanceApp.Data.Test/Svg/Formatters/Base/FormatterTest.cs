
using PerformanceApp.Data.Svg.Formatters.Base;

namespace PerformanceApp.Data.Test.Svg.Formatters.Base;

public class FormatterTest
{
    [Fact]
    public void Format_SingleValue_ReturnsFormattedString()
    {
        // Arrange
        var formatString = "F2";
        var formatter = new Formatter(formatString);
        var value = 123.456f;

        // Act
        var result = formatter.Format(value);

        // Assert
        Assert.Equal("123.46", result);
    }

    [Fact]
    public void Format_MultipleValues_ReturnsFormattedStrings()
    {
        // Arrange
        var formatString = "F1";
        var formatter = new Formatter(formatString);
        var values = new List<float> { 1.234f, 5.678f, 9.101f };

        // Act
        var results = formatter.Format(values).ToList();

        // Assert
        var expected = new List<string> { "1.2", "5.7", "9.1" };
        Assert.Equal(expected, results);
    }
}