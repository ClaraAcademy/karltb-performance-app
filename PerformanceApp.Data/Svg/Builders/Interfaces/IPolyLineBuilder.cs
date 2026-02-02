using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Builders.Interfaces;

public interface IPolyLineBuilder
{
    IPolyLineBuilder WithPoints(IEnumerable<string> points);
    IPolyLineBuilder WithColor(string color);
    IPolyLineBuilder WithWidth(int width);
    IPolyLineBuilder IsDotted(bool dotted = true);
    XElement Build();
}