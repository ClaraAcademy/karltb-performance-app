using System.Xml.Linq;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class TickFactoryTest
{
    static string Format(float f) => DecimalFormatter.Format(f);
    const int Offset = TickDefaults.Length / 2;
    [Fact]
    public void Ticks_ShouldReturnElements_ForGivenCoordinates()
    {
        // Arrange
        var coordinates = new List<float> { 1f, 2f, 3f };
        Func<float, XElement> createTick = x => new XElement("tick", new XAttribute("pos", x));
        var factory = new TickFactory(coordinates, createTick);

        // Act
        var ticks = factory.Ticks.ToList();

        // Assert
        Assert.Equal(3, ticks.Count);
        Assert.Equal("tick", ticks[0].Name.LocalName);
        Assert.Equal("1", ticks[0].Attribute("pos")?.Value);
        Assert.Equal("2", ticks[1].Attribute("pos")?.Value);
        Assert.Equal("3", ticks[2].Attribute("pos")?.Value);
    }

    [Fact]
    public void CreateX_ShouldBuildTicksWithCorrectY()
    {
        // Arrange
        var xs = new List<float> { 10f, 20f };
        float y0 = 5f;

        // Act
        var factory = TickFactory.CreateX(xs, y0);
        var ticks = factory.Ticks.ToList();

        // Assert
        Assert.Equal(2, ticks.Count);
        foreach (var (tick, x) in ticks.Zip(xs, (t, x) => (t, x)))
        {
            Assert.Equal("line", tick.Name.LocalName);
            Assert.Equal(Format(x), tick.Attribute("x1")?.Value);
            Assert.Equal(Format(x), tick.Attribute("x2")?.Value);
            Assert.Equal(Format(y0 - Offset), tick.Attribute("y1")?.Value);
            Assert.Equal(Format(y0 + Offset), tick.Attribute("y2")?.Value);
        }
    }

    [Fact]
    public void CreateY_ShouldBuildTicksWithCorrectX()
    {
        // Arrange
        var ys = new List<float> { 15f, 25f };
        float x0 = 7f;

        // Act
        var factory = TickFactory.CreateY(ys, x0);
        var ticks = factory.Ticks.ToList();

        // Assert
        Assert.Equal(2, ticks.Count);
        foreach (var (tick, y) in ticks.Zip(ys, (t, y) => (t, y)))
        {
            Assert.Equal("line", tick.Name.LocalName);
            Assert.Equal(Format(y), tick.Attribute("y1")?.Value);
            Assert.Equal(Format(y), tick.Attribute("y2")?.Value);
            Assert.Equal(Format(x0 - Offset), tick.Attribute("x1")?.Value);
            Assert.Equal(Format(x0 + Offset), tick.Attribute("x2")?.Value);
        }
    }
}