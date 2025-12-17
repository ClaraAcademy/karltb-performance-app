using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class PolyLineFactory(string color, int width, bool dotted)
{
    private readonly string _color = color;
    private readonly int _width = width;
    private readonly bool _dotted = dotted;

    public XElement GetSvgPolyline(List<string> points)
    {
        var joined = SvgUtilities.MapToString(points);

        var element = new XElementBuilder(XElementConstants.Polyline)
            .WithAttribute(XAttributeConstants.Points, joined)
            .WithAttribute(XAttributeConstants.Fill, XAttributeConstants.None)
            .WithAttribute(XAttributeConstants.Stroke, _color)
            .WithAttribute(XAttributeConstants.StrokeWidth, _width);

        if (_dotted)
        {
            var spacing = SvgUtilities.MapToPoint(2, 2);
            element = element
                .WithAttribute(XAttributeConstants.StrokeDashArray, spacing);
        }

        return element.Build();
    }
}
