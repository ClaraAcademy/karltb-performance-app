using PerformanceApp.Data.Svg.Formatters.Base;

namespace PerformanceApp.Data.Svg.Formatters;

public class PercentageFormatter
{
    public static string Format(float value) => Formatter.Format(value, "P0");
}