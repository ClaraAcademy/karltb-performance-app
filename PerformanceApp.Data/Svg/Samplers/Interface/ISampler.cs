namespace   PerformanceApp.Data.Svg.Samplers.Interface;

public interface ISampler<T>
{
    List<T> Samples { get; }
}