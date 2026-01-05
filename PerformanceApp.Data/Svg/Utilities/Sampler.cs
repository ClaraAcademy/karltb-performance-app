using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Utilities;

public class Sampler<T>(IScaler scaler, Func<T, string> selector)
{
    private readonly IScaler _scaler = scaler;
    private readonly Func<T, string> _selector = selector;

    public List<(float, string)> Sample(IEnumerable<T> data, int count)
    {
        var list = data.ToList();
        var step = (float)(list.Count - 1) / (count - 1);
        var samples = Enumerable
            .Range(0, count)
            .Select(i => i * step)
            .ToList();
        var labels = samples
            .Select(s => Math.Round(s))
            .Select(s => (int)s)
            .Select(i => list[i])
            .Select(_selector);
        var values = samples
            .Select(_scaler.Scale);
        var result = values
            .Zip(labels, (value, label) => (value, label))
            .ToList();

        return result;
    }
}