using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;

namespace PerformanceApp.Data.Svg.Factories;

public class LabelFactory(IEnumerable<float> coordinates, IEnumerable<string> strings, Func<float, string, XElement> createLabel)
{
    public IEnumerable<XElement> Labels => coordinates.Zip(strings, createLabel);

    public static LabelFactory CreateX(IEnumerable<float> xs, IEnumerable<int> indexes, Func<int, string> toString, float y0)
    {
        var strings = indexes.Select(toString);
        XElement toLabel(float x, string text) => LabelBuilder.BuildX(x, y0, text);
        return new LabelFactory(xs, strings, toLabel);
    }
    public static LabelFactory CreateY(IEnumerable<float> ys, IEnumerable<float> values, Func<float, string> toString, float x0)
    {
        var strings = values.Select(toString);
        XElement toLabel(float y, string text) => LabelBuilder.BuildY(x0, y, text);
        return new LabelFactory(ys, strings, toLabel);
    }
}