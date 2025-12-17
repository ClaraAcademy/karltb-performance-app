using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Factories;

public class LabelFactory()
{
    private readonly TextFactory _textFactory = new();

    public XElement GetLabel(
        float x,
        float y,
        string labelText,
        string anchor = "middle",
        float angle = 0
    )
    {
        return _textFactory.Create(labelText, x, y, anchor, angle);
    }
}