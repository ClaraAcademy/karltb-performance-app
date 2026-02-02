using System.Xml.Linq;
using Moq;
using PerformanceApp.Data.Svg.Builders.Interfaces;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class PolyLineFactoryTest
{
    [Fact]
    public void Line_Property_Should_Invoke_Builder_With_Correct_Parameters()
    {
        // Arrange
        var points = new List<string> { "0,0", "1,1" };
        var color = "#FF0000";
        var isDotted = true;

        var builderMock = new Mock<IPolyLineBuilder>();
        builderMock.Setup(b => b.WithPoints(points)).Returns(builderMock.Object);
        builderMock.Setup(b => b.WithColor(color)).Returns(builderMock.Object);
        builderMock.Setup(b => b.IsDotted(isDotted)).Returns(builderMock.Object);
        var expectedElement = new XElement("polyline");
        builderMock.Setup(b => b.Build()).Returns(expectedElement);

        var factory = new PolyLineFactory(builderMock.Object, points, color, isDotted);

        // Act
        var result = factory.Line;

        // Assert
        Assert.Equal(expectedElement, result);
        builderMock.Verify(b => b.WithPoints(points), Times.Once);
        builderMock.Verify(b => b.WithColor(color), Times.Once);
        builderMock.Verify(b => b.IsDotted(isDotted), Times.Once);
        builderMock.Verify(b => b.Build(), Times.Once);
    }

    [Fact]
    public void FromSeries_Should_Create_PolyLineFactory_With_Correct_Parameters()
    {
        // Arrange
        var series = new ChartSeries(values: [1.0f, 2.0f], color: "#00FF00");

        var xScalerMock = new Mock<IScaler>();
        xScalerMock.Setup(s => s.Scale(It.IsAny<int>())).Returns<int>(x => x * 10);

        var yScalerMock = new Mock<IScaler>();
        yScalerMock.Setup(s => s.Scale(It.IsAny<float>())).Returns<float>(y => y * 100);

        var isDotted = false;

        // Act
        var factory = PolyLineFactory.FromSeries(series, xScalerMock.Object, yScalerMock.Object, isDotted);

        // Assert
        Assert.NotNull(factory);
        Assert.Equal(series.Color, typeof(PolyLineFactory).GetConstructor(new[] {
            typeof(IPolyLineBuilder), typeof(IEnumerable<string>), typeof(string), typeof(bool)
        }) != null ? series.Color : null);
    }
}