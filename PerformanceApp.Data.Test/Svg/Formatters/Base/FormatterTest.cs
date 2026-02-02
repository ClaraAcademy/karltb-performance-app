using PerformanceApp.Data.Svg.Formatters.Base;

namespace PerformanceApp.Data.Test.Svg.Formatters.Base;

public class FormatterTest
{
    [Theory]
    [InlineData(1234.5678f, "F2", "1234.57")]
    [InlineData(0.123456f, "F3", "0.123")]
    [InlineData(-42.42f, "F1", "-42.4")]
    [InlineData(1000f, "N0", "1,000")]
    [InlineData(3.14159f, "0.00", "3.14")]
    public void Format_ReturnsExpectedString(float value, string format, string expected)
    {
        var result = Formatter.Format(value, format);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Format_UsesInvariantCulture()
    {
        float value = 1234.5f;
        string format = "N1";
        var result = Formatter.Format(value, format);
        // InvariantCulture uses ',' as thousand separator and '.' as decimal separator
        Assert.Equal("1,234.5", result);
    }
}