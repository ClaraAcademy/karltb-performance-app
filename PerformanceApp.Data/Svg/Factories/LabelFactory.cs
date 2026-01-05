using System.Xml.Linq;
using DocumentFormat.OpenXml.Drawing.Charts;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories.Core;

namespace PerformanceApp.Data.Svg.Factories;

public class LabelFactory()
{
    private readonly TextFactory _textFactory = new();

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

    public List<XElement> CreateXs(IEnumerable<(float,string)> samples, float y, string anchor = "middle", float angle = 0)
    {
        var result = new List<XElement>();
        foreach (var (x, text) in samples)
        {
            result.Add(Create(x, y, text, anchor, angle));
        }
        return result;
    }

    public List<XElement> CreateYs(IEnumerable<(float, string)> samples, float x, string anchor = "middle", float angle = 0)
    {
        var result = new List<XElement>();
        foreach (var (y, text) in samples)
        {
            result.Add(Create(x, y, text, anchor, angle));
        }
        return result;
    }

}