using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories;

public class PolyLineFactory(string color, int width, bool dotted)
{
    private readonly string _color = color;
    private readonly int _width = width;
    private readonly bool _dotted = dotted;

    public XElement GetSvgPolyline(List<string> points)
    {
        var joined = SvgUtilities.MapToString(points);

        var attributes = new List<(string, string)>
        {
            (XAttributeConstants.Points, joined),
            (XAttributeConstants.Fill, XAttributeConstants.None),
            (XAttributeConstants.Stroke, _color),
            (XAttributeConstants.StrokeWidth, _width.ToString())
        };

        if (_dotted)
        {
            var spacing = SvgUtilities.MapToPoint(2, 2);
            attributes.Add((XAttributeConstants.StrokeDashArray, spacing));
        }

        return XElementFactory.Create(XElementConstants.Polyline, attributes);
    }
}
