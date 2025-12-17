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
    private readonly int _length = TickDefaults.Length;
    private readonly XScaler _xScaler;
    private readonly YScaler _yScaler;

    public TickFactory(XScaler xScaler, YScaler yScaler)
    {
        _xScaler = xScaler;
        _yScaler = yScaler;
        _color = ColorConstants.Black;
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

    public XElement CreateX(float x, float y)
    {
        return Create(x, y - _length / 2, x, y + _length / 2);
    }

    public IEnumerable<XElement> CreateXs(int total, int nTicks)
    {
        var samples = Sampler.Sample(total, nTicks);
        var xs = _xScaler.Scale(samples);
        var y = _yScaler.Scale(0);

        return xs
            .Skip(1) // Skip first to avoid overlapping with axis line
            .Select(x => CreateX(x, y));
    }

    public XElement CreateY(float x, float y)
    {
        return Create(x - _length / 2, y, x + _length / 2, y);
    }

    public IEnumerable<XElement> CreateYs(int total, int nTicks)
    {
        var samples = Sampler.Sample(total, nTicks);
        var ys = _yScaler.Scale(samples);
        var x = _xScaler.Scale(0);

        return ys
            .Skip(1) // Skip first to avoid overlapping with axis line
            .Select(y => CreateY(x, y));
    }
}