using Moq;
using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Svg.Extractors;
using PerformanceApp.Data.Svg.Factories;
using PerformanceApp.Data.Svg.Scalers.Interface;
using PerformanceApp.Data.Svg.Utilities;

namespace PerformanceApp.Data.Test.Svg.Factories;

public class PointFactoryTest
{
    private readonly Mock<IScaler> _xScalerMock;
    private readonly Mock<IScaler> _yScalerMock;
    private readonly XExtractor _xExtractor;
    private readonly YExtractor _yExtractor;

    public PointFactoryTest()
    {
        _xScalerMock = new Mock<IScaler>();
        _xScalerMock.Setup(s => s.Scale(It.IsAny<int>())).Returns<float>(x => x);
        _yScalerMock = new Mock<IScaler>();
        _yScalerMock.Setup(s => s.Scale(It.IsAny<float>())).Returns<float>(y => y);
        _xExtractor = new XExtractor(_xScalerMock.Object);
        _yExtractor = new YExtractor(_yScalerMock.Object);
    }
    static DataPoint2 CreateDataPoint(int i)
    {
        return new DataPoint2(new DateOnly(2024, 1, i), i * 10f, i * 100f);
    }
}