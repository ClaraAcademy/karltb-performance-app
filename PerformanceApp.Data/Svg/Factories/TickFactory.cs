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

    public IEnumerable<XElement> CreateXs(int count, float y)
    {
        var y1 = y - _offset;
        var y2 = y + _offset;
        var xs = Enumerable
            .Range(1, count)
            .Select(_xScaler.Scale)
            .Select(x => (float)x);

        return xs.Select(x => Create(x, y1, x, y2));
    }

    public IEnumerable<XElement> CreateYs(int count, float x)
    {
        var x1 = x - _offset;
        var x2 = x + _offset;
        var ys = Enumerable
            .Range(1, count)
            .Select(_yScaler.Scale)
            .Select(y => (float)y);

        return ys.Select(y => Create(x1, y, x2, y));
    }
}