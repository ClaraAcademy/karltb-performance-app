using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Enums;
using PerformanceApp.Data.Svg.Formatters;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class LabelBuilderTest
{
    [Fact]
    public void BuildXs_ShouldBuildCorrectElements()
    {
        // Arrange
        var texts = new List<string> { "Label1", "Label2", "Label3" };
        var xs = new List<float> { 10f, 20f, 30f };
        var builder = new LabelBuilder()
            .WithTexts(texts)
            .WithY(50f)
            .WithAngle(0)
            .WithAnchor(Anchor.Middle)
            .WithXs(xs);

        // Act
        var elements = builder.BuildXs();

        // Assert
        Assert.Equal(3, elements.Count);
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            Assert.Equal("text", element.Name.LocalName);
            Assert.Equal(DecimalFormatter.Format(xs[i]), element.Attribute("x")?.Value);
            Assert.Equal("50.00", element.Attribute("y")?.Value);
            Assert.Equal("middle", element.Attribute("text-anchor")?.Value);
            Assert.Equal(texts[i], element.Value);
        }
    }

    [Fact]
    public void BuildYs_ShouldBuildCorrectElements()
    {
        // Arrange
        var texts = new List<string> { "LabelA", "LabelB", "LabelC" };
        var ys = new List<float> { 15f, 25f, 35f };
        var builder = new LabelBuilder()
            .WithTexts(texts)
            .WithX(100f)
            .WithAngle(0)
            .WithAnchor(Anchor.Start)
            .WithYs(ys);

        // Act
        var elements = builder.BuildYs();

        // Assert
        Assert.Equal(3, elements.Count);
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            Assert.Equal("text", element.Name.LocalName);
            Assert.Equal("100.00", element.Attribute("x")?.Value);
            Assert.Equal(DecimalFormatter.Format(ys[i]), element.Attribute("y")?.Value);
            Assert.Equal("start", element.Attribute("text-anchor")?.Value);
            Assert.Equal(texts[i], element.Value);
        }
    }

}