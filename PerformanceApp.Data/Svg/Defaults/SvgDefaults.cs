using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Common;

namespace PerformanceApp.Data.Svg.Defaults;

public class SvgDefaults
{
    public static readonly Margins Margins = new(X: 50, Y: 50);
    public static readonly Samples Samples = new(X: 5, Y: 5);
    public static readonly XNamespace Namespace = "http://www.w3.org/2000/svg";
    public static XElementBuilder CreateBaseSchema(int width, int height)
    {
        return new XElementBuilder("svg")
            .WithAttribute("width", width)
            .WithAttribute("height", height);
    }
}