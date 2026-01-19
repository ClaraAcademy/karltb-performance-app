using System.Xml.Linq;
using PerformanceApp.Data.Svg.Common;

namespace PerformanceApp.Data.Svg.Defaults;

public class SvgDefaults
{
    public static readonly Margins Margins = new(X: 50, Y: 50);
    public static readonly Samples Samples = new(X: 5, Y: 5);
    public class Color
    {
        public const string Primary = "#211f5e";
        public const string Secondary = "#ec646b";
    }
    public static readonly XNamespace Namespace = "http://www.w3.org/2000/svg";
}