using System.Globalization;

namespace PerformanceApp.Data.Svg.Formatters;

public class PercentageFormatter
{
    public static string Format(float value) => value.ToString("0 %", CultureInfo.InvariantCulture);
}