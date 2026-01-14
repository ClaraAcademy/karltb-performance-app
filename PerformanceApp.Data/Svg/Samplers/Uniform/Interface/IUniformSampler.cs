namespace PerformanceApp.Data.Svg.Samplers.Uniform.Interface;

public interface IUniformSampler<T>
{
    List<T> Sample();
    T Transform(int index);
}