using System.Globalization;

namespace PerformanceApp.Data.Svg.Formatters.Base;

public class Formatter(string formatString)
{
    private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    private readonly string _formatString = formatString;

    public string Format(float value)
    {
        return value.ToString(_formatString, Culture);
    }
    public IEnumerable<string> Format(IEnumerable<float> values)
    {
        return values.Select(Format);
    }
}
