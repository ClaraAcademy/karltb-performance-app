using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory
{
    private readonly ILineFactory _lineFactory = new LineFactory(ColorConstants.Black, 1);
    private readonly int _offset = TickDefaults.Length / 2;

    public XElement Create(float x1, float y1, float x2, float y2)
    {
        return _lineFactory.Create(x1, y1, x2, y2);
    }

    public List<XElement> CreateXs(IEnumerable<float> xs, float y)
    {
        var y1 = y - _offset;
        var y2 = y + _offset;

        return xs
            .Select(x => Create(x, y1, x, y2))
            .ToList();
    }

    public List<XElement> CreateYs(IEnumerable<float> ys, float x)
    {
        var x1 = x - _offset;
        var x2 = x + _offset;

        return ys
            .Select(y => Create(x1, y, x2, y))
            .ToList();
    }
}