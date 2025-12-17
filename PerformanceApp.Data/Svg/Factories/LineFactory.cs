using System.Numerics;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories;

public class LineFactory<T>
    where T : INumber<T>
{
    private readonly string _color;
    private readonly int _width;
    private readonly DecimalFormatter<T> _decimalFormatter;

    public LineFactory(string color, int width)
    {
        _color = color;
        _width = width;
        _decimalFormatter = new DecimalFormatter<T>();
    }

    public LineFactory()
    {
        _color = LineConstants.DefaultColor;
        _width = LineConstants.DefaultWidth;
        _decimalFormatter = new DecimalFormatter<T>();
    }

    public XElement Create(T x1, T y1, T x2, T y2)
    {
        var formatted = _decimalFormatter
            .Format([x1, y1, x2, y2]);
        var values = new[]
        {
            formatted.ElementAt(0),
            formatted.ElementAt(1),
            formatted.ElementAt(2),
            formatted.ElementAt(3),
            _color,
            _width.ToString()
        };

        var attributes = LineConstants.AttributeNames
            .Zip(values)
            .Select(XAttributeFactory.Create);

        return XElementFactory.Create(LineConstants.ElementName, attributes);
    }
}