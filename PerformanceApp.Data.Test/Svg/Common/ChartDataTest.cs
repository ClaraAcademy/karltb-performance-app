using PerformanceApp.Data.Svg.Common;

namespace PerformanceApp.Data.Test.Svg.Common;

public class ChartDataTest
{
    private class TestChartSeries(List<float> values, string color = "black") : ChartSeries(values, color)
    {
        public new int Count => Values.Count;
        public new float Max => Values.Count > 0 ? Values.Max() : float.MinValue;
        public new float Min => Values.Count > 0 ? Values.Min() : float.MaxValue;
        public new List<float> Values { get; } = values;
    }

    [Fact]
    public void GetXLabel_ReturnsCorrectLabel()
    {
        var xs = new List<string> { "A", "B", "C" };
        var series = new List<ChartSeries>();
        var chartData = new ChartData(xs, series);

        Assert.Equal("A", chartData.GetXLabel(0));
        Assert.Equal("B", chartData.GetXLabel(1));
        Assert.Equal("C", chartData.GetXLabel(2));
    }

    [Fact]
    public void GetXLabel_ReturnsEmptyString_WhenIndexIsOutOfRange()
    {
        var xs = new List<string> { "A" };
        var series = new List<ChartSeries>();
        var chartData = new ChartData(xs, series);

        Assert.Throws<ArgumentOutOfRangeException>(() => chartData.GetXLabel(2));
    }

    [Fact]
    public void SeriesProperty_ReturnsSeries()
    {
        var xs = new List<string>();
        var s = new TestChartSeries([1, 2, 3]);
        var series = new List<ChartSeries> { s };
        var chartData = new ChartData(xs, series);

        Assert.Single(chartData.Series);
        Assert.Equal(s, chartData.Series[0]);
    }

    [Fact]
    public void PointCount_ReturnsMaxCountOfSeries()
    {
        var xs = new List<string>();
        var s1 = new TestChartSeries([1, 2]);
        var s2 = new TestChartSeries([1, 2, 3, 4]);
        var s3 = new TestChartSeries([1]);
        var chartData = new ChartData(xs, [s1, s2, s3]);

        Assert.Equal(4, chartData.PointCount);
    }

    [Fact]
    public void Max_ReturnsMaxOfAllSeries()
    {
        var xs = new List<string>();
        var s1 = new TestChartSeries([1, 2]);
        var s2 = new TestChartSeries([5, 3]);
        var chartData = new ChartData(xs, [s1, s2]);

        Assert.Equal(5, chartData.Max);
    }

    [Fact]
    public void Min_ReturnsMinOfAllSeries()
    {
        var xs = new List<string>();
        var s1 = new TestChartSeries([1, 2]);
        var s2 = new TestChartSeries([5, -3]);
        var chartData = new ChartData(xs, [s1, s2]);

        Assert.Equal(-3, chartData.Min);
    }
}