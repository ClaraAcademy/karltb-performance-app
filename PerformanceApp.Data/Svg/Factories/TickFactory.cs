using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory(IEnumerable<float> coordinates, Func<float, XElement> createTick)
{
    public IEnumerable<XElement> Ticks => coordinates.Select(createTick);

    public static TickFactory CreateX(IEnumerable<float> xs, float y0) => new(xs, x => TickBuilder.BuildX(x, y0));
    public static TickFactory CreateY(IEnumerable<float> ys, float x0) => new(ys, y => TickBuilder.BuildY(x0, y));
}