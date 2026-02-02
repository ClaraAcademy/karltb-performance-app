using Xunit;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Scalers.Index;
using PerformanceApp.Data.Svg.Scalers.Value;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class ScalerFactoryTest
{
    [Fact]
    public void X_Returns_IndexScaler_With_Correct_Parameters()
    {
        // Arrange
        var dimensions = new Dimensions(100, 200);
        var margins = new Margins(10, 20);
        int pointCount = 5;

        // Act
        var factory = new ScalerFactory(dimensions, margins, pointCount, 0, 0);
        var xScaler = factory.X;

        // Assert
        Assert.IsType<IndexScaler>(xScaler);
        Assert.Equal(dimensions.X, ((IndexScaler)xScaler).Length);
        Assert.Equal(margins.X, ((IndexScaler)xScaler).Margin);
        Assert.Equal(pointCount, ((IndexScaler)xScaler).Total);
    }

    [Fact]
    public void Y_Returns_ValueScaler_With_Correct_Parameters_And_Inverted()
    {
        // Arrange
        var dimensions = new Dimensions(100, 200);
        var margins = new Margins(10, 20);
        int pointCount = 5;
        float maxValue = 10f;
        float minValue = 0f;

        // Act
        var factory = new ScalerFactory(dimensions, margins, pointCount, maxValue, minValue);
        var yScaler = factory.Y;

        // Assert
        Assert.IsType<ValueScaler>(yScaler);
        Assert.Equal(dimensions.Y, ((ValueScaler)yScaler).Length);
        Assert.Equal(margins.Y, ((ValueScaler)yScaler).Margin);
        Assert.Equal(maxValue, ((ValueScaler)yScaler).Max);
        Assert.Equal(minValue, ((ValueScaler)yScaler).Min);
        Assert.True(((ValueScaler)yScaler).Inverted);
    }

    [Fact]
    public void Create_Returns_ScalerFactory_With_Correct_Parameters()
    {
        // Arrange
        var data = new TestChartData(max: 100f, min: 0f);
        var dimensions = new Dimensions(300, 400);
        var margins = new Margins(30, 40);

        // Act
        var factory = ScalerFactory.Create(data, dimensions, margins);

        // Assert
        Assert.NotNull(factory);
        Assert.IsType<ScalerFactory>(factory);
        Assert.IsType<IndexScaler>(factory.X);
        Assert.IsType<ValueScaler>(factory.Y);
    }
}
class TestChartData(float max, float min)
    : ChartData([], [new ChartSeries([max, min], "#000000")])
{ }