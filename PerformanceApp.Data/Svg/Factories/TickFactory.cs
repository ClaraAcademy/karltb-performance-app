using System.Numerics;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Scalers;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory
{
    private readonly string _color;
    private readonly LineFactory _lineFactory;
    private readonly int _length;
    private readonly int _offset;
    private readonly XScaler _xScaler;
    private readonly YScaler _yScaler;

    public TickFactory(XScaler xScaler, YScaler yScaler)
        : this(xScaler, yScaler, TickDefaults.Length) { }

    public TickFactory(XScaler xScaler, YScaler yScaler, int length)
    {
        _xScaler = xScaler;
        _yScaler = yScaler;
        _color = ColorConstants.Black;
        _length = length;
        _offset = length / 2;
        _lineFactory = new LineFactory(_color, 1);
    }

    public TickFactory(string color, XScaler xScaler, YScaler yScaler)
    {
        _xScaler = xScaler;
        _yScaler = yScaler;
        _color = color;
        _lineFactory = new LineFactory(_color, 1);
    }

    public XElement Create(float x1, float y1, float x2, float y2)
    {
        return _lineFactory.Create(x1, y1, x2, y2);
    }

    public IEnumerable<XElement> CreateXs(IEnumerable<float> xs, float y)
    {
        return xs.Select(x => Create(x, y - _offset, x, y + _offset));
    }

    public IEnumerable<XElement> CreateYs(IEnumerable<float> ys, float x)
    {
        return ys.Select(y => Create(x - _offset, y, x + _offset, y));
    }
}