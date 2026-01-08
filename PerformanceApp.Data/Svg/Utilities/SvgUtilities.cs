using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Utilities;

public static class SvgUtilities
{
    private static readonly DecimalFormatter _decimalFormatter = new();

    public static string MapToPoint(float x, float y)
    {
        var fx = _decimalFormatter.Format(x);
        var fy = _decimalFormatter.Format(y);
        return $"{fx},{fy}";
    }

    public static string MapToString(List<string> points)
    {
        return string.Join(" ", points);
    }

    public static List<string> MapToPoints(List<float> xs, List<float> ys)
    {
        return xs
            .Zip(ys, MapToPoint)
            .ToList();
    }
}