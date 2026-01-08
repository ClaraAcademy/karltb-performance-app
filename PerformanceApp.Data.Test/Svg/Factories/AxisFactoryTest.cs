using System.Linq.Expressions;
using System.Xml.Linq;
using Moq;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Factories.Core.Interfaces;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class AxisFactoryTest
{
    private readonly Mock<IScaler> _xScalerMock;
    private readonly Mock<IScaler> _yScalerMock;
    private readonly Mock<ILineFactory> _lineFactoryMock;

    class Defaults
    {
        public const string TestLine ="test-line";
        public class Inputs
        {
            public const string X1 = "x1";
            public const string Y1 = "y1";
            public const string X2 = "x2";
            public const string Y2 = "y2";
        }
    }

    private static float Identity(float f) => f;
    private static Expression<Func<IScaler, float>> AnyFloat() => s => s.Scale(It.IsAny<float>());

    static XElement LineFactoryReturn(float f1, float f2, float f3, float f4)
    {
        return new XElement(Defaults.TestLine,
            new XAttribute(Defaults.Inputs.X1, f1),
            new XAttribute(Defaults.Inputs.Y1, f2),
            new XAttribute(Defaults.Inputs.X2, f3),
            new XAttribute(Defaults.Inputs.Y2, f4));
    }

    public AxisFactoryTest()
    {
        _xScalerMock = new Mock<IScaler>();
        _xScalerMock.Setup(AnyFloat()).Returns(Identity);
        _yScalerMock = new Mock<IScaler>();
        _yScalerMock.Setup(AnyFloat()).Returns(Identity);
        _lineFactoryMock = new Mock<ILineFactory>();
        _lineFactoryMock
            .Setup(lf => lf.Create(It.IsAny<float>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<float>()))
            .Returns(LineFactoryReturn);
    }

    [Fact]
    public void CreateX_ReturnsCorrectXElement()
    {
        // Arrange
        var width = 200;
        var height = 100;
        var axisFactory = new AxisFactory(_xScalerMock.Object, _yScalerMock.Object, _lineFactoryMock.Object, width, height);

        // Act
        var xAxis = axisFactory.CreateX();

        // Assert
        Assert.Equal(Defaults.TestLine, xAxis.Name.LocalName);
        Assert.Equal("0", xAxis.Attribute(Defaults.Inputs.X1)?.Value);
        Assert.Equal("0", xAxis.Attribute(Defaults.Inputs.Y1)?.Value);
        Assert.Equal(width.ToString(), xAxis.Attribute(Defaults.Inputs.X2)?.Value);
        Assert.Equal("0", xAxis.Attribute(Defaults.Inputs.Y2)?.Value);
    }

    [Fact]
    public void CreateY_ReturnsCorrectXElement()
    {
        // Arrange
        var width = 200;
        var height = 100;
        var axisFactory = new AxisFactory(_xScalerMock.Object, _yScalerMock.Object, _lineFactoryMock.Object, width, height);

        // Act
        var yAxis = axisFactory.CreateY();

        // Assert
        Assert.Equal(Defaults.TestLine, yAxis.Name.LocalName);
        Assert.Equal("0", yAxis.Attribute(Defaults.Inputs.X1)?.Value);
        Assert.Equal("0", yAxis.Attribute(Defaults.Inputs.Y1)?.Value);
        Assert.Equal("0", yAxis.Attribute(Defaults.Inputs.X2)?.Value);
        Assert.Equal(height.ToString(), yAxis.Attribute(Defaults.Inputs.Y2)?.Value);
    }
}