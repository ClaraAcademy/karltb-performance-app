using System.Xml.Linq;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;

namespace PerformanceApp.Data.Svg.Factories;

public class LabelFactory(int length)
{
    private readonly TextFactory _textFactory = new();
    private readonly int _length = length;

    public LabelFactory() : this(LabelDefaults.Length) { }

    public XElement Create(
        float x,
        float y,
        string text,
        string anchor = "middle",
        float angle = 0
    )
    {
        return _textFactory.Create(text, x, y, anchor, angle);
    }

    public IEnumerable<XElement> CreateXs(
        IEnumerable<float> xs,
        float y,
        IEnumerable<string> texts,
        string anchor = "middle",
        float angle = 0
    )
    {
        return xs.Zip(texts, (x, text) => Create(x, y, text, anchor, angle));
    }

    public IEnumerable<XElement> CreateYs(
        IEnumerable<float> ys,
        float x,
        IEnumerable<string> texts,
        string anchor = "middle",
        float angle = 0
    )
    {
        return ys.Zip(texts, (y, text) => Create(x, y, text, anchor, angle));
    }


}