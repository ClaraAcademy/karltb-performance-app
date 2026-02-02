using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories;

public class PointFactory(Func<float, float, string> toPoint, IEnumerable<float> xs, IEnumerable<float> ys)
{
    public IEnumerable<string> Points => xs.Zip(ys, toPoint);
    public static PointFactory Default(IEnumerable<float> xs, IEnumerable<float> ys)
    {
        static string Format(float value) => DecimalFormatter.Format(value);
        static string ToPoint(float x, float y) => $"{Format(x)},{Format(y)}";
        return new PointFactory(ToPoint, xs, ys);
    }
}