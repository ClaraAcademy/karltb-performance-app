using System.Xml.Linq;
using PerformanceApp.Data.Svg.Defaults;

namespace PerformanceApp.Data.Svg.Builders;

public class TickBuilder
{
    static float Offset => TickDefaults.Length / 2;
    static XElement BuildTick(float x1, float y1, float x2, float y2)
    {
        return new LineBuilder()
            .WithColor("black")
            .WithWidth(1)
            .From(x1, y1)
            .To(x2, y2)
            .Build();
    }
    public static List<XElement> BuildXs(IEnumerable<float> xs, float y0)
    {
        var y1 = y0 - Offset;
        var y2 = y0 + Offset;
        return xs
            .Select(x => BuildTick(x, y1, x, y2))
            .ToList();
    }
    public static List<XElement> BuildYs(IEnumerable<float> ys, float x0)
    {
        var x1 = x0 - Offset;
        var x2 = x0 + Offset;
        return ys
            .Select(y => BuildTick(x1, y, x2, y))
            .ToList();
    }
}