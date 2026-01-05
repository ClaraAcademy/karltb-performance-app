using System.Numerics;
using PerformanceApp.Data.Svg.Formatters.Abstract;

namespace PerformanceApp.Data.Svg.Formatters;

public class PercentageFormatter: Formatter
{
    public static new string Format(float value)
    {
        return value.ToString("F2", Culture);
    }
}