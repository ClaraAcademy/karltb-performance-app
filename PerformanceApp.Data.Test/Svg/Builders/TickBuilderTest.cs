using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class TickBuilderTest
{
    private readonly Func<float, string> _format = DecimalFormatter.Format;
    [Fact]
    public void BuildX_CreatesVerticalLineWithCorrectCoordinates()
    {
        float x = 10f;
        float y0 = 20f;
        float offset = TickDefaults.Length / 2;

        XElement element = TickBuilder.BuildX(x, y0);

        Assert.Equal("line", element.Name.LocalName);
        Assert.Equal(_format(x), element.Attribute("x1")?.Value);
        Assert.Equal(_format(y0 - offset), element.Attribute("y1")?.Value);
        Assert.Equal(_format(x), element.Attribute("x2")?.Value);
        Assert.Equal(_format(y0 + offset), element.Attribute("y2")?.Value);
        Assert.Equal("black", element.Attribute("stroke")?.Value);
        Assert.Equal("1", element.Attribute("stroke-width")?.Value);
    }

    [Fact]
    public void BuildY_CreatesHorizontalLineWithCorrectCoordinates()
    {
        float x0 = 15f;
        float y = 25f;
        float offset = TickDefaults.Length / 2;

        XElement element = TickBuilder.BuildY(x0, y);

        Assert.Equal("line", element.Name.LocalName);
        Assert.Equal(_format(x0 - offset), element.Attribute("x1")?.Value);
        Assert.Equal(_format(y), element.Attribute("y1")?.Value);
        Assert.Equal(_format(x0 + offset), element.Attribute("x2")?.Value);
        Assert.Equal(_format(y), element.Attribute("y2")?.Value);
        Assert.Equal("black", element.Attribute("stroke")?.Value);
        Assert.Equal("1", element.Attribute("stroke-width")?.Value);
    }
}