using System.Globalization;
using System.Numerics;

namespace PerformanceApp.Data.Svg.Formatters.Abstract;

public abstract class Formatter<T>
    where T : INumber<T>
{
    protected static readonly CultureInfo Culture = CultureInfo.InvariantCulture;
    public abstract string Format(T value);
}
