
using PerformanceApp.Data.Dtos;

namespace PerformanceApp.Data.Mappers;

public static class DataPoint2Mapper
{
    public static List<DataPoint> FlattenY1s(IEnumerable<DataPoint2> dataPoints)
    {
        return Flatten(dataPoints, d => d.Y1);
    }

    public static List<DataPoint> FlattenY2s(IEnumerable<DataPoint2> dataPoints)
    {
        return Flatten(dataPoints, d => d.Y2);
    }

    private static List<DataPoint> Flatten(IEnumerable<DataPoint2> dataPoints, Func<DataPoint2, float> selector)
    {
        return dataPoints
            .Select(d => new DataPoint(d.X, selector(d)))
            .ToList();
    }
}