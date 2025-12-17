using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Svg.Factories;

public class TextFactory(string anchor, float angle, int size)
{
    private readonly string _anchor = anchor;
    private readonly float _angle = angle;
    private readonly int _size = size;
    private readonly DecimalFormatter<float> _decimalFormatter = new();

    public TextFactory() : this("middle", 0, TextDefaults.FontSize) { }
    public TextFactory(float angle) : this("middle", angle, TextDefaults.FontSize) { }
    public TextFactory(string anchor, float angle) : this(anchor, angle, TextDefaults.FontSize) { }

    string Rotate(string x, string y) => $"rotate({_angle} {x},{y})";

    public XElement Create(string text, float x, float y)
    {
        var fx = _decimalFormatter.Format(x);
        var fy = _decimalFormatter.Format(y);

        var attributes = new List<(string, string)>
        {
            (XAttributeConstants.X, fx),
            (XAttributeConstants.Y, fy),
            (XAttributeConstants.FontSize, _size.ToString()),
            (XAttributeConstants.TextAnchor, _anchor),
            (XAttributeConstants.Transform, Rotate(fx, fy))
        };

        return XElementFactory.Create(XElementConstants.Text, text, attributes);
    }

}
