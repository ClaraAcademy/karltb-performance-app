using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Svg.Factories;

public class AxisFactory(float x0, float y0, float nX, float nY)
{
    public XElement X => AxisBuilder.Build((0, y0), (nX, y0));
    public XElement Y => AxisBuilder.Build((x0, 0), (x0, nY));
}