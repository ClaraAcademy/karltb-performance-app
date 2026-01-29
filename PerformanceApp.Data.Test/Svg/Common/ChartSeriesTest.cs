using System.Globalization;
using PerformanceApp.Data.Svg.Common;

namespace PerformanceApp.Data.Test.Svg.Common;

public class ChartSeriesTest
{
    [Fact]
    public void Constructor_SetsValuesAndColor()
    {
        var values = new float[] { 1.2f, 3.4f, 5.6f };
        var color = "#FF0000";
        var series = new ChartSeries(values, color);

        Assert.Equal(values, series.Values);
        Assert.Equal(color, series.Color);
    }

    [Fact]
    public void Count_ReturnsCorrectNumberOfValues()
    {
        var values = new float[] { 1.0f, 2.0f, 3.0f };
        var series = new ChartSeries(values, "blue");

        Assert.Equal(3, series.Count);
    }

    [Fact]
    public void Max_ReturnsMaximumValue()
    {
        var values = new float[] { 2.5f, 7.1f, 3.3f };
        var series = new ChartSeries(values, "green");

        Assert.Equal(7.1f, series.Max);
    }

    [Fact]
    public void Min_ReturnsMinimumValue()
    {
        var values = new float[] { 2.5f, 7.1f, 3.3f };
        var series = new ChartSeries(values, "green");

        Assert.Equal(2.5f, series.Min);
    }

    [Fact]
    public void From_CreatesChartSeriesFromGenericEnumerable()
    {
        var values = new[] { "1.1", "2.2", "3.3" };
        var series = ChartSeries.From(values, s => float.Parse(s, CultureInfo.InvariantCulture), "red");

        Assert.Equal(new List<float> { 1.1f, 2.2f, 3.3f }, series.Values);
        Assert.Equal("red", series.Color);
    }

    [Fact]
    public void Max_ThrowsOnEmptyValues()
    {
        var series = new ChartSeries(Array.Empty<float>(), "black");
        Assert.Throws<InvalidOperationException>(() => { var _ = series.Max; });
    }

    [Fact]
    public void Min_ThrowsOnEmptyValues()
    {
        var series = new ChartSeries(Array.Empty<float>(), "black");
        Assert.Throws<InvalidOperationException>(() => { var _ = series.Min; });
    }
}