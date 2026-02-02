namespace PerformanceApp.Data.Svg.Formatters.Base;

public sealed class Formatter
{
    public static string Format(float value, string format)
    {
        return value.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
    }
}