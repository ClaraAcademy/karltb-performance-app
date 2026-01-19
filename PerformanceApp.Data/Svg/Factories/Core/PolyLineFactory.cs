using System.Xml.Linq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class PolyLineFactory(IEnumerable<DataPoint> dataPoints, IScaler xScaler, IScaler yScaler, string color, int width, bool dotted)
{
    private readonly PointFactory _pointFactory = new(dataPoints, xScaler, yScaler);
    private readonly string _color = color;
    private readonly int _width = width;
    private readonly bool _dotted = dotted;

    public XElement PolyLine => new PolyLineBuilder()
        .WithColor(_color)
        .WithWidth(_width)
        .IsDotted(_dotted)
        .WithPoints(_pointFactory.Points)
        .Build();
}
