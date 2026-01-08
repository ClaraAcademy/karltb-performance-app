using System.Linq.Expressions;
using System.Xml.Linq;
using Moq;
using PerformanceApp.Data.Svg.Constants;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Factories.Core;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class LabelFactoryTest
{
    private readonly Mock<ITextFactory> _textFactoryMock;

    private class Defaults
    {
        public static XElement TextFactoryReturn(string text, float x, float y, string anchor, float angle)
        {
            return new XElement("text",
                new XAttribute("x", x),
                new XAttribute("y", y),
                new XAttribute("text-anchor", anchor),
                new XAttribute("transform", $"rotate({angle},{x},{y})"), text);
        }
    }

    public LabelFactoryTest()
    {
        _textFactoryMock = new Mock<ITextFactory>();
        _textFactoryMock
            .Setup(tf => tf.Create(It.IsAny<string>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<string>(), It.IsAny<float>()))
            .Returns(Defaults.TextFactoryReturn);
    }

    [Fact]
    public void Create_ShouldReturnExpectedElement()
    {
        // Arrange
        var x = 100f;
        var y = 200f;
        var text = "Test Label";
        var anchor = "start";
        var angle = 45f;

        var labelFactory = new LabelFactory(_textFactoryMock.Object);

        // Act
        var result = labelFactory.Create(x, y, text, anchor, angle);

        // Assert
        Assert.Equal("text", result.Name.LocalName);
        Assert.Equal(x.ToString(), result.Attribute("x")?.Value);
        Assert.Equal(y.ToString(), result.Attribute("y")?.Value);
        Assert.Equal(anchor, result.Attribute("text-anchor")?.Value);
        Assert.Equal($"rotate({angle},{x},{y})", result.Attribute("transform")?.Value);
        Assert.Equal(text, result.Value);
    }

    [Fact]
    public void CreateXs_ShouldReturnEmptyList_WhenNoSamplesProvided()
    {
        // Arrange
        var labelFactory = new LabelFactory(_textFactoryMock.Object);
        var samples = new List<(float, string)>();
        var y = 150f;

        // Act
        var result = labelFactory.CreateXs(samples, y);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void CreateXs_ShouldReturnExpectedElements()
    {
        // Arrange
        var labelFactory = new LabelFactory(_textFactoryMock.Object);
        var samples = new List<(float, string)>
        {
            (50f, "Label 1"),
            (100f, "Label 2"),
            (150f, "Label 3")
        };
        var y = 150f;

        // Act
        var result = labelFactory.CreateXs(samples, y);

        // Assert
        Assert.Equal(samples.Count, result.Count);
        for (int i = 0; i < samples.Count; i++)
        {
            var (x, text) = samples[i];
            var element = result[i];

            Assert.Equal("text", element.Name.LocalName);
            Assert.Equal(x.ToString(), element.Attribute("x")?.Value);
            Assert.Equal(y.ToString(), element.Attribute("y")?.Value);
            Assert.Equal(text, element.Value);
        }
    }

    [Fact]
    public void CreateYs_ShouldReturnEmptyList_WhenNoSamplesProvided()
    {
        // Arrange
        var labelFactory = new LabelFactory(_textFactoryMock.Object);
        var samples = new List<(float, string)>();
        var x = 200f;

        // Act
        var result = labelFactory.CreateYs(samples, x);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void CreateYs_ShouldReturnExpectedElements()
    {
        // Arrange
        var labelFactory = new LabelFactory(_textFactoryMock.Object);
        var samples = new List<(float, string)>
        {
            (50f, "Label A"),
            (100f, "Label B"),
            (150f, "Label C")
        };
        var x = 200f;

        // Act
        var result = labelFactory.CreateYs(samples, x);

        // Assert
        Assert.Equal(samples.Count, result.Count);
        for (int i = 0; i < samples.Count; i++)
        {
            var (y, text) = samples[i];
            var element = result[i];

            Assert.Equal("text", element.Name.LocalName);
            Assert.Equal(x.ToString(), element.Attribute("x")?.Value);
            Assert.Equal(y.ToString(), element.Attribute("y")?.Value);
            Assert.Equal(text, element.Value);
        }
    }


}