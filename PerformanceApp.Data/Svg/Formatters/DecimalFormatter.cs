using PerformanceApp.Data.Svg.Formatters.Base;

namespace PerformanceApp.Data.Svg.Formatters;

public class DecimalFormatter
{
    public static string Format(float value) => Formatter.Format(value, "0.00");
}