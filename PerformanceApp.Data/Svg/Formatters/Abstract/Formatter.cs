using System.Globalization;
using System.Numerics;

namespace PerformanceApp.Data.Svg.Formatters.Abstract;

public abstract class Formatter
{
    protected static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    public static string Format(float value)
    {
        return value.ToString(Culture);
    }
    public static IEnumerable<string> Format(IEnumerable<float> values)
    {
        return values.Select(Format);
    }
}
