using PerformanceApp.Data.Dtos;

namespace PerformanceApp.Data.Extensions;

public static class DataPoint2Extensions
{
    public static List<float> GetY1s(this IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints.Select(dp => dp.Y1).ToList();
    }
    public static List<float> GetY2s(this IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints.Select(dp => dp.Y2).ToList();
    }
    public static List<DateOnly> GetXs(this IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints.Select(dp => dp.X).ToList();
    }
    public static List<float> GetYs(this IEnumerable<DataPoint2> dataPoints)
    {
        var y1s = dataPoints.GetY1s();
        var y2s = dataPoints.GetY2s();
        return y1s.Concat(y2s).ToList();
    }
}