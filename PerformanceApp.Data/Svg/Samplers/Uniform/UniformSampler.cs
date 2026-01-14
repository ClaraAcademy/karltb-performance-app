using PerformanceApp.Data.Svg.Samplers.Uniform.Interface;

namespace PerformanceApp.Data.Svg.Samplers.Uniform;

public abstract class UniformSampler<T>(int count)
    : IUniformSampler<T>
{
    protected readonly int _count = count;

    public abstract T Transform(int index);
    public List<T> Sample()
    {
        return Enumerable
            .Range(0, _count)
            .Select(Transform)
            .ToList();
    }
}