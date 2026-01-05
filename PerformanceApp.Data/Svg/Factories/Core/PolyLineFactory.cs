using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class PolyLineFactory
{
    public static XElement Create(
        List<string> points, 
        string color = ColorConstants.Black, 
        int width = 1, 
        bool dotted = false
    )
    {
        var joined = SvgUtilities.MapToString(points);

        var element = new XElementBuilder(XElementConstants.Polyline)
            .WithAttribute(XAttributeConstants.Points, joined)
            .WithAttribute(XAttributeConstants.Fill, XAttributeConstants.None)
            .WithAttribute(XAttributeConstants.Stroke, color)
            .WithAttribute(XAttributeConstants.StrokeWidth, width);

        if (dotted)
        {
            var spacing = SvgUtilities.MapToPoint(2, 2);
            element = element
                .WithAttribute(XAttributeConstants.StrokeDashArray, spacing);
        }

        return element.Build();
    }

    public static XElement CreatePrimary(
        List<string> points,
        string color = SvgDefaults.PrimaryColor,
        int width = 2,
        bool dotted = false
    )
    {
        return Create(points, color, width, dotted);
    }

    public static XElement CreateSecondary(
        List<string> points,
        string color = SvgDefaults.SecondaryColor,
        int width = 2,
        bool dotted = true
    )
    {
        return Create(points, color, width, dotted);
    }
}
