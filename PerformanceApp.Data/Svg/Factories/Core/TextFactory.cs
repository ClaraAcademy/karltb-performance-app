using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
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

        return new XElementBuilder(XElementConstants.Text)
            .WithAttribute(XAttributeConstants.X, fx)
            .WithAttribute(XAttributeConstants.Y, fy)
            .WithAttribute(XAttributeConstants.FontSize, _size)
            .WithAttribute(XAttributeConstants.TextAnchor, anchor)
            .WithAttribute(XAttributeConstants.Transform, Rotate(fx, fy, angle))
            .WithValue(text)
            .Build();
    }

    static string Rotate(string x, string y, float angle) => $"rotate({angle} {x},{y})";
}
