using Moq;
using PerformanceApp.Data.Svg.Samplers;
using PerformanceApp.Data.Svg.Samplers.Coordinate;

namespace PerformanceApp.Data.Test.Svg.Samplers;

public class XSamplerTest
{
    static string LabelSelector<T>(T item) => $"Label for {item}";
}
