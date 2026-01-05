using PerformanceApp.Data.Svg.Formatters.Abstract;

namespace PerformanceApp.Data.Svg.Formatters;

public class PercentageFormatter: Formatter
{
    public override string Format(float value)
    {
        return value.ToString("P0", Culture);
    }

}