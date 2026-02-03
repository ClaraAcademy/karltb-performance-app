using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Common;

namespace PerformanceApp.Data.Extensions;

public static class DataPoint2Extensions
{
    public static ChartData ToChartData(this IEnumerable<DataPoint2> dataPoints, string color1, string color2)
    {
        var xs = dataPoints.GetXs().Select(d => d.ToString()).ToList();
        var ys = dataPoints.ToSeriesY(color1, color2);
        return new ChartData(xs, ys);
    }

    public static ChartSeries ToSeriesY1(this IEnumerable<DataPoint2> dataPoints, string color) => dataPoints.ToSeriesY(d => d.Y1, color);
    public static ChartSeries ToSeriesY2(this IEnumerable<DataPoint2> dataPoints, string color) => dataPoints.ToSeriesY(d => d.Y2, color);
    private static ChartSeries ToSeriesY(this IEnumerable<DataPoint2> dataPoints, Func<DataPoint2, float> selector, string color)
    {
        return ChartSeries.From(dataPoints, selector, color);
    }
    public static List<ChartSeries> ToSeriesY(this IEnumerable<DataPoint2> dataPoints, string color1, string color2)
    {
        return [dataPoints.ToSeriesY1(color1), dataPoints.ToSeriesY2(color2)];
    }
    public static List<DateOnly> GetXs(this IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints.Select(dp => dp.X).ToList();
    }
}