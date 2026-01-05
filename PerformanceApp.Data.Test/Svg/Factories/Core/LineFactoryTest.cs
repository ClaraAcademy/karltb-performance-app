using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Factories.Core;

public class LineFactoryTest
{
    private const float X1 = 1f;
    private const float Y1 = 10f;
    private const float X2 = 2f;
    private const float Y2 = 20f;
    private const string Color = "#ff006aff";
    private const int Width = 9;

    [Theory]
    [InlineData(Color, Width)]
    [InlineData(null, null)]
    [InlineData(null, Width)]
    [InlineData(Color, null)]
    public void Create_ProducesCorrectLineElement(string? color, int? width)
    {
        var c = color ?? ColorConstants.Black;
        var w = width ?? LineConstants.DefaultWidth;
        // Arrange
        var factory = new LineFactory(c, w);

        // Act
        var lineElement = factory.Create(X1, Y1, X2, Y2);

        // Assert
        AssertEqual(lineElement, c, w);
    }

    private static void AssertEqual(XElement actual, string color, int width)
    {
        var decimalFormatter = new DecimalFormatter();
        Assert.Equal(XElementConstants.Line, actual.Name.LocalName);
        Assert.Equal(decimalFormatter.Format(X1), actual.Attribute("x1")?.Value);
        Assert.Equal(decimalFormatter.Format(Y1), actual.Attribute("y1")?.Value);
        Assert.Equal(decimalFormatter.Format(X2), actual.Attribute("x2")?.Value);
        Assert.Equal(decimalFormatter.Format(Y2), actual.Attribute("y2")?.Value);
        Assert.Equal(color, actual.Attribute("stroke")?.Value);
        Assert.Equal(width.ToString(), actual.Attribute("stroke-width")?.Value);
    }
}