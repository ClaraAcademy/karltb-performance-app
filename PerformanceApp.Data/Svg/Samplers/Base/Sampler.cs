using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Samplers.Base;

public abstract class Sampler<T>(IScaler scaler, Func<T, string> selector)
{
    protected readonly IScaler _scaler = scaler;
    protected readonly Func<T, string> _selector = selector;

    public abstract List<(float, string)> Sample(IEnumerable<T> data, int count);
}