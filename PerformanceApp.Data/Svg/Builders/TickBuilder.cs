using System.Xml.Linq;
using PerformanceApp.Data.Svg.Defaults;

namespace PerformanceApp.Data.Svg.Builders;

public class TickBuilder
{
    const float Offset = TickDefaults.Length / 2;
    static XElement Build(float x1, float y1, float x2, float y2)
    {
        return new LineBuilder()
            .WithColor("black")
            .WithWidth(1)
            .From(x1, y1)
            .To(x2, y2)
            .Build();
    }
    public static XElement BuildX(float x, float y0) => Build(x, y0 - Offset, x, y0 + Offset);
    public static XElement BuildY(float x0, float y) => Build(x0 - Offset, y, x0 + Offset, y);
}