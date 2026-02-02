using System.Xml.Linq;
using Moq;
using PerformanceApp.Data.Svg.Builders.Interfaces;
using PerformanceApp.Data.Svg.Factories;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class AxisFactoryTest
{
    class TestAxisBuilder : IAxisBuilder
    {
        private float _x1, _y1, _x2, _y2;

        public IAxisBuilder From(float x1, float y1) { _x1 = x1; _y1 = y1; return this; }
        public IAxisBuilder To(float x2, float y2) { _x2 = x2; _y2 = y2; return this; }
        public XElement Build()
        {
            return new XElement("line",
                new XAttribute("x1", _x1),
                new XAttribute("y1", _y1),
                new XAttribute("x2", _x2),
                new XAttribute("y2", _y2));
        }
    }
    [Fact]
    public void X_ShouldReturnXAxisLine()
    {
        // Arrange
        float x0 = 10;
        float y0 = 20;
        float nX = 200;
        float nY = 150;
        var axisFactory = new AxisFactory(new TestAxisBuilder(), x0, y0, nX, nY);

        // Act
        var xAxis = axisFactory.X;

        // Assert
        Assert.NotNull(xAxis);
        Assert.Equal(0f.ToString(), xAxis.Attribute("x1")?.Value);
        Assert.Equal(y0.ToString(), xAxis.Attribute("y1")?.Value);
        Assert.Equal(nX.ToString(), xAxis.Attribute("x2")?.Value);
        Assert.Equal(y0.ToString(), xAxis.Attribute("y2")?.Value);
    }
    [Fact]
    public void Y_ShouldReturnYAxisLine()
    {
        // Arrange
        float x0 = 10;
        float y0 = 20;
        float nX = 200;
        float nY = 150;
        var axisFactory = new AxisFactory(new TestAxisBuilder(), x0, y0, nX, nY);

        // Act
        var yAxis = axisFactory.Y;

        // Assert
        Assert.NotNull(yAxis);
        Assert.Equal(x0.ToString(), yAxis.Attribute("x1")?.Value);
        Assert.Equal(0f.ToString(), yAxis.Attribute("y1")?.Value);
        Assert.Equal(x0.ToString(), yAxis.Attribute("x2")?.Value);
        Assert.Equal(nY.ToString(), yAxis.Attribute("y2")?.Value);
    }
}