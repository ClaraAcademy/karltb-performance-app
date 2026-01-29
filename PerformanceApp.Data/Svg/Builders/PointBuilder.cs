using System.Drawing;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Builders;

public class PointBuilder
{
    static readonly Func<float, string> Format = DecimalFormatter.Format;
    PointF Point { get; set; } = new PointF(0f, 0f);
    public PointBuilder At(PointF point)
    {
        Point = point;
        return this;
    }
    public PointBuilder At(float x, float y) => At(new(x, y));
    public string Build() => $"{Format(Point.X)},{Format(Point.Y)}";

    public static string Build(float x, float y)
    {
        return new PointBuilder()
            .At(x, y)
            .Build();
    }
    public static IEnumerable<string> Build(IEnumerable<float> xs, IEnumerable<float> ys) => xs.Zip(ys, Build);
}