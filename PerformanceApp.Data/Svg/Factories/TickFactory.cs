using System.Numerics;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory<T>
    where T : INumber<T>
{
    private readonly string _color;
    private readonly LineFactory<T> _lineFactory;
    private const int Width = 1;

    public TickFactory() : this(ColorConstants.Black) { }

    public TickFactory(string color)
    {
        _color = color;
        _lineFactory = new LineFactory<T>(_color, Width);
    }

    public XElement Create(T x1, T y1, T x2, T y2) => _lineFactory.Create(x1, y1, x2, y2);
}