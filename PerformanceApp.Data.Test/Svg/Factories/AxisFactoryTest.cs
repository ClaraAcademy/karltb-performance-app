using System.Xml.Linq;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class AxisFactoryTest
{
    private readonly Func<float, string> _format = DecimalFormatter.Format;
    [Fact]
    public void X_Property_ShouldReturnXAxisElement()
    {
        // Arrange
        float x0 = 10, y0 = 20, nX = 100, nY = 200;
        var factory = new AxisFactory(x0, y0, nX, nY);

        // Act
        XElement xAxis = factory.X;

        // Assert
        Assert.NotNull(xAxis);
        Assert.Equal("line", xAxis.Name.LocalName);
        Assert.Equal(_format(0), xAxis.Attribute("x1")?.Value);
        Assert.Equal(_format(y0), xAxis.Attribute("y1")?.Value);
        Assert.Equal(_format(nX), xAxis.Attribute("x2")?.Value);
        Assert.Equal(_format(y0), xAxis.Attribute("y2")?.Value);
    }

    [Fact]
    public void Y_Property_ShouldReturnYAxisElement()
    {
        // Arrange
        float x0 = 10, y0 = 20, nX = 100, nY = 200;
        var factory = new AxisFactory(x0, y0, nX, nY);

        // Act
        XElement yAxis = factory.Y;

        // Assert
        Assert.NotNull(yAxis);
        Assert.Equal("line", yAxis.Name.LocalName);
        Assert.Equal(_format(x0), yAxis.Attribute("x1")?.Value);
        Assert.Equal(_format(0), yAxis.Attribute("y1")?.Value);
        Assert.Equal(_format(x0), yAxis.Attribute("x2")?.Value);
        Assert.Equal(_format(nY), yAxis.Attribute("y2")?.Value);
    }
}