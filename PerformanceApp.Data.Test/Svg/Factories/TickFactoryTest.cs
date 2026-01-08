using System.Xml.Linq;
using Moq;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class TickFactoryTest
{
    private readonly Mock<ILineFactory> _lineFactoryMock;

    public TickFactoryTest()
    {
        _lineFactoryMock = new Mock<ILineFactory>();
        _lineFactoryMock
            .Setup(lf => lf.Create(It.IsAny<float>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<float>()))
            .Returns<float, float, float, float>((x1, y1, x2, y2) =>
                new XElement("line",
                    new XAttribute("x1", x1),
                    new XAttribute("y1", y1),
                    new XAttribute("x2", x2),
                    new XAttribute("y2", y2)
                ));
    }

    [Fact]
    public void Create_ReturnsCorrectResult()
    {
        // Arrange
        var tickFactory = new TickFactory(_lineFactoryMock.Object);
        var x1 = 1f;
        var y1 = 2f;
        var x2 = 3f;
        var y2 = 4f;

        // Act
        var actual = tickFactory.Create(x1, y1, x2, y2);

        // Assert
        Assert.Equal("line", actual.Name.LocalName);
        Assert.Equal(x1, float.Parse(actual.Attribute("x1")!.Value));
        Assert.Equal(y1, float.Parse(actual.Attribute("y1")!.Value));
        Assert.Equal(x2, float.Parse(actual.Attribute("x2")!.Value));
        Assert.Equal(y2, float.Parse(actual.Attribute("y2")!.Value));
    }

    [Fact]
    public void CreateXs_ReturnsCorrectResults()
    {
        // Arrange
        var tickFactory = new TickFactory(_lineFactoryMock.Object, 10);
        var xs = new List<float> { 1f, 2f, 3f };
        var y = 5f;
        var expectedY1 = y - 5f;
        var expectedY2 = y + 5f;

        // Act
        var actual = tickFactory.CreateXs(xs, y).ToList();

        // Assert
        Assert.Equal(xs.Count, actual.Count);
        for (int i = 0; i < xs.Count; i++)
        {
            var line = actual[i];
            Assert.Equal(xs[i], float.Parse(line.Attribute("x1")!.Value));
            Assert.Equal(expectedY1, float.Parse(line.Attribute("y1")!.Value));
            Assert.Equal(xs[i], float.Parse(line.Attribute("x2")!.Value));
            Assert.Equal(expectedY2, float.Parse(line.Attribute("y2")!.Value));
        }
    }

    [Fact]
    public void CreateYs_ReturnsCorrectResults()
    {
        // Arrange
        var tickFactory = new TickFactory(_lineFactoryMock.Object, 10);
        var ys = new List<float> { 1f, 2f, 3f };
        var x = 5f;
        var expectedX1 = x - 5f;
        var expectedX2 = x + 5f;

        // Act
        var actual = tickFactory.CreateYs(ys, x).ToList();

        // Assert
        Assert.Equal(ys.Count, actual.Count);
        for (int i = 0; i < ys.Count; i++)
        {
            var line = actual[i];
            Assert.Equal(expectedX1, float.Parse(line.Attribute("x1")!.Value));
            Assert.Equal(ys[i], float.Parse(line.Attribute("y1")!.Value));
            Assert.Equal(expectedX2, float.Parse(line.Attribute("x2")!.Value));
            Assert.Equal(ys[i], float.Parse(line.Attribute("y2")!.Value));
        }
    }
}