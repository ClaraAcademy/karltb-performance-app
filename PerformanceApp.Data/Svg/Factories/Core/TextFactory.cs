using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class TextFactory(int size)
{
    private readonly int _size = size;
    private readonly DecimalFormatter _decimalFormatter = new();

    public TextFactory() : this(TextDefaults.FontSize) { }

    public XElement Create(
        string text,
        float x,
        float y,
        string anchor = "middle",
        float angle = 0
    )
    {
        var fx = _decimalFormatter.Format(x);
        var fy = _decimalFormatter.Format(y);

        var attributes = new List<(string, string)>
        {
            (XAttributeConstants.X, fx),
            (XAttributeConstants.Y, fy),
            (XAttributeConstants.FontSize, _size.ToString()),
            (XAttributeConstants.TextAnchor, anchor),
            (XAttributeConstants.Transform, Rotate(fx, fy, angle))
        };

        return XElementFactory.Create(XElementConstants.Text, text, attributes);
    }

    static string Rotate(string x, string y, float angle) => $"rotate({angle} {x},{y})";
}
