using System.Numerics;
using PerformanceApp.Data.Svg.Formatters.Abstract;

namespace PerformanceApp.Data.Svg.Formatters;

public class PercentageFormatter<T> : Formatter<T>
    where T : INumber<T>
{
    public override string Format(T value)
    {
        return value.ToString("F2", Culture);
    }
}