using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory(ILineFactory lineFactory, int length)
{
    private readonly ILineFactory _lineFactory = lineFactory;
    private readonly int _offset = length / 2;

    private class Defaults
    {
        public const string Color = ColorConstants.Black;
        public const int Length = 10;
        public static readonly ILineFactory LineFactory = new LineFactory(Color, 1);
    }

    public TickFactory() : this(Defaults.LineFactory, TickDefaults.Length) { }
    public TickFactory(int length) : this(Defaults.LineFactory, length) { }
    public TickFactory(ILineFactory lineFactory) : this(lineFactory, Defaults.Length) { }

    public XElement Create(float x1, float y1, float x2, float y2)
    {
        return _lineFactory.Create(x1, y1, x2, y2);
    }

    public IEnumerable<XElement> CreateXs(IEnumerable<float> xs, float y)
    {
        var y1 = y - _offset;
        var y2 = y + _offset;

        return xs.Select(x => Create(x, y1, x, y2));
    }

    public IEnumerable<XElement> CreateYs(IEnumerable<float> ys, float x)
    {
        var x1 = x - _offset;
        var x2 = x + _offset;

        return ys.Select(y => Create(x1, y, x2, y));
    }
}