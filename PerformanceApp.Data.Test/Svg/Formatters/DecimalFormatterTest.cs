using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Formatters;

public class DecimalFormatterTest
{
    [Fact]
    public void Format_SingleValue_ReturnsFormattedString()
    {
        // Arrange
        var formatter = new DecimalFormatter();
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
        var formatter = new DecimalFormatter();
        var values = new List<float> { 123.456f, 78.9f, 0.1234f };

        // Act
        var results = formatter.Format(values).ToList();

        // Assert
        Assert.Equal(["123.46", "78.90", "0.12"], results);
    }
}