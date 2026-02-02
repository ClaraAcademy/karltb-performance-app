using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Builders.Interfaces;

namespace PerformanceApp.Data.Svg.Factories;

public class AxisFactory(IAxisBuilder axisBuilder, float x0, float y0, float nX, float nY)
{
    public static AxisFactory Create(float x0, float y0, float nX, float nY)
    {
        var builder = new AxisBuilder();
        return new(builder, x0, y0, nX, nY);
    }

    public XElement X => axisBuilder.From(0, y0).To(nX, y0).Build();
    public XElement Y => axisBuilder.From(x0, 0).To(x0, nY).Build();
}