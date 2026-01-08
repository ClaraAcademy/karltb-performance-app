using System.Globalization;

namespace PerformanceApp.Data.Svg.Formatters.Base;

public class Formatter(string formatString, CultureInfo culture)
{
    private readonly CultureInfo Culture = culture;
    private readonly string _formatString = formatString;

    public Formatter(string formatString) 
        : this(formatString, CultureInfo.InvariantCulture)
    {
    }

    public string Format(float value)
    {
        return value.ToString(_formatString, Culture);
    }
    public IEnumerable<string> Format(IEnumerable<float> values)
    {
        return values.Select(Format);
    }
}
