using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Builders.Interfaces;

public interface IAxisBuilder
{
    IAxisBuilder From(float x1, float y1);
    IAxisBuilder To(float x2, float y2);
    XElement Build();
}