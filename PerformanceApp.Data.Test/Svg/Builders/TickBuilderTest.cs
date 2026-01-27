using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Defaults;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class TickBuilderTest
{
    [Fact]
    public void BuildXs_ShouldBuildCorrectElements()
    {
        // Arrange
        var xs = new List<float> { 10f, 20f, 30f };
        var y0 = 50f;

        // Act
        var elements = TickBuilder.BuildXs(xs, y0);

        // Assert
        Assert.Equal(3, elements.Count);
        var offset = TickDefaults.Length / 2;
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            Assert.Equal("line", element.Name.LocalName);
            Assert.Equal(DecimalFormatter.Format(xs[i]), element.Attribute("x1")?.Value);
            Assert.Equal(DecimalFormatter.Format(y0 - offset), element.Attribute("y1")?.Value);
            Assert.Equal(DecimalFormatter.Format(xs[i]), element.Attribute("x2")?.Value);
            Assert.Equal(DecimalFormatter.Format(y0 + offset), element.Attribute("y2")?.Value);
            Assert.Equal("black", element.Attribute("stroke")?.Value);
            Assert.Equal("1", element.Attribute("stroke-width")?.Value);
        }
    }

    [Fact]
    public void BuildYs_ShouldBuildCorrectElements()
    {
        // Arrange
        var ys = new List<float> { 15f, 25f, 35f };
        var x0 = 100f;

        // Act
        var elements = TickBuilder.BuildYs(ys, x0);

        // Assert
        Assert.Equal(3, elements.Count);
        var offset = TickDefaults.Length / 2;
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            Assert.Equal("line", element.Name.LocalName);
            Assert.Equal(DecimalFormatter.Format(x0 - offset), element.Attribute("x1")?.Value);
            Assert.Equal(DecimalFormatter.Format(ys[i]), element.Attribute("y1")?.Value);
            Assert.Equal(DecimalFormatter.Format(x0 + offset), element.Attribute("x2")?.Value);
            Assert.Equal(DecimalFormatter.Format(ys[i]), element.Attribute("y2")?.Value);
            Assert.Equal("black", element.Attribute("stroke")?.Value);
            Assert.Equal("1", element.Attribute("stroke-width")?.Value);
        }
    }
}