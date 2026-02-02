using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Formatters;

public class PercentageFormatterTest
{
    [Theory]
    [InlineData(0f, "0 %")]
    [InlineData(1f, "100 %")]
    [InlineData(0.5f, "50 %")]
    [InlineData(0.1234f, "12 %")]
    [InlineData(-0.25f, "-25 %")]
    public void Format_ReturnsExpectedPercentageString(float value, string expected)
    {
        var result = PercentageFormatter.Format(value);
        Assert.Equal(expected, result);
    }
}