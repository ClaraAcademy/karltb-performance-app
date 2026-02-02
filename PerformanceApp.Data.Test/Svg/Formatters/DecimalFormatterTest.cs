using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Formatters;

public class DecimalFormatterTest
{
    [Theory]
    [InlineData(0f, "0.00")]
    [InlineData(1f, "1.00")]
    [InlineData(-1f, "-1.00")]
    [InlineData(123.4567f, "123.46")]
    [InlineData(0.004f, "0.00")]
    [InlineData(0.005f, "0.01")]
    [InlineData(-0.005f, "-0.01")]
    public void Format_ReturnsExpectedString(float value, string expected)
    {
        var result = DecimalFormatter.Format(value);
        Assert.Equal(expected, result);
    }
}