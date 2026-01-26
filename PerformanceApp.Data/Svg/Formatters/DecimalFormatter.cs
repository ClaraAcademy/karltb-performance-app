using System.Globalization;

namespace PerformanceApp.Data.Svg.Formatters;

public class DecimalFormatter
{
    public static string Format(float value) => value.ToString("0.00", CultureInfo.InvariantCulture);
}