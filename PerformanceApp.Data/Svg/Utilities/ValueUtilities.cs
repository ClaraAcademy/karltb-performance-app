using PerformanceApp.Data.Dtos;

namespace PerformanceApp.Data.Svg.Utilities;

public class ValueUtilities
{
    public static float MinY(DataPoint2 dataPoint)
    {
        return Math.Min(dataPoint.Y1, dataPoint.Y2);
    }

    public static float MinY(IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints.Min(MinY);
    }

    public static float MaxY(DataPoint2 dataPoint)
    {
        return Math.Max(dataPoint.Y1, dataPoint.Y2);
    }

    public static float MaxY(IEnumerable<DataPoint2> dataPoints)
    {
        return dataPoints.Max(MaxY);
    }
}