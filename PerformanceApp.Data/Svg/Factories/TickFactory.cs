using System.Numerics;
using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories;

public class TickFactory<T>
    where T : INumber<T>
{
    private readonly string _color;
    private readonly DecimalFormatter<T> _decimalFormatter;

    public TickFactory()
    {
        _color = ColorConstants.Black;
        _decimalFormatter = new DecimalFormatter<T>();
    }

    public XElement Create(T x1, T y1, T x2, T y2)
    {
        var attributes = new[]
        {
            (XAttributeConstants.X1, _decimalFormatter.Format(x1)),
            (XAttributeConstants.Y1, _decimalFormatter.Format(y1)),
            (XAttributeConstants.X2, _decimalFormatter.Format(x2)),
            (XAttributeConstants.Y2, _decimalFormatter.Format(y2)),
            (XAttributeConstants.Stroke, _color)
        };

        return XElementFactory.Create(XElementConstants.Line, attributes);
    }
}