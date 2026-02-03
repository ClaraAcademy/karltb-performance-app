using System.Xml.Linq;
using Moq;
using PerformanceApp.Data.Svg.Common;
using PerformanceApp.Data.Svg.Samplers;
using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Test.Svg.Samplers;

public class SamplerTest
{
    [Fact]
    public void CreateX_ReturnsSamplerWithExpectedTicksAndLabels()
    {
        // Arrange
        var data = new TestChartData(max: 50f, min: 0f);
        var scalerMock = new Mock<IScaler>();
        scalerMock.Setup(s => s.Scale(It.IsAny<int>())).Returns<int>(i => i * 10f);

        // Act
        var sampler = Sampler.CreateX(data, scalerMock.Object, 3, 5f);

        // Assert
        Assert.NotNull(sampler);
        Assert.NotEmpty(sampler.Ticks);
        Assert.NotEmpty(sampler.Labels);
        Assert.All(sampler.Labels, l => Assert.IsType<XElement>(l));
        Assert.All(sampler.Ticks, t => Assert.IsType<XElement>(t));
    }

    [Fact]
    public void CreateY_ReturnsSamplerWithExpectedTicksAndLabels()
    {
        // Arrange
        var data = new TestChartData(max: 50f, min: 0f);
        var scalerMock = new Mock<IScaler>();
        scalerMock.Setup(s => s.Scale(It.IsAny<float>())).Returns<float>(v => v * 2);

        // Act
        var sampler = Sampler.CreateY(data, scalerMock.Object, 4, 10f);

        // Assert
        Assert.NotNull(sampler);
        Assert.NotEmpty(sampler.Ticks);
        Assert.NotEmpty(sampler.Labels);
        Assert.All(sampler.Labels, l => Assert.IsType<XElement>(l));
        Assert.All(sampler.Ticks, t => Assert.IsType<XElement>(t));
    }
}


class TestChartData(float max, float min)
    : ChartData(CreateXs(100), [new([max, min], "#000000")])
{
    private static IEnumerable<string> CreateXs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return i.ToString();
        }
    }


}