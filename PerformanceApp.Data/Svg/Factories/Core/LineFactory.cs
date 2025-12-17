using System.Numerics;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class LineFactory
{
    private readonly string _color;
    private readonly int _width;
    private readonly DecimalFormatter _decimalFormatter;

    public LineFactory(string color, int width)
    {
        _color = color;
        _width = width;
        _decimalFormatter = new DecimalFormatter();
    }

    public LineFactory()
    {
        _color = ColorConstants.Black;
        _width = LineConstants.DefaultWidth;
        _decimalFormatter = new DecimalFormatter();
    }

    public XElement Create(float x1, float y1, float x2, float y2)
    {
        var attributes = new[]
        {
            (XAttributeConstants.X1, _decimalFormatter.Format(x1)),
            (XAttributeConstants.Y1, _decimalFormatter.Format(y1)),
            (XAttributeConstants.X2, _decimalFormatter.Format(x2)),
            (XAttributeConstants.Y2, _decimalFormatter.Format(y2)),
            (XAttributeConstants.Stroke, _color),
            (XAttributeConstants.StrokeWidth, _width.ToString())
        };

        return XElementFactory.Create(XElementConstants.Line, attributes);
    }
}