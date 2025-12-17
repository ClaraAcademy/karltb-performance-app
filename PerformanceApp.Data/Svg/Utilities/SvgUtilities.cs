using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Utilities;

public static class SvgUtilities
{
    private static readonly DecimalFormatter<float> _decimalFormatter = new();
    public static string MapToPoint(float x, float y)
    {
        var fx = _decimalFormatter.Format(x);
        var fy = _decimalFormatter.Format(y);
        return $"{fx},{fy}";
    }
}