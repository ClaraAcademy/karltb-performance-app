using System.Numerics;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory
{
    private readonly string _color;
    private readonly LineFactory _lineFactory;
    private const int Width = 1;

    public TickFactory() : this(ColorConstants.Black) { }

    public TickFactory(string color)
    {
        _color = color;
        _lineFactory = new LineFactory(_color, Width);
    }

    public XElement Create(float x1, float y1, float x2, float y2)
    {
        return _lineFactory.Create(x1, y1, x2, y2);
    }
}