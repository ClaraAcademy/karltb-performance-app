using PerformanceApp.Data.Svg.Samplers.Base;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Samplers;

public class YSampler<T>(YScaler yScaler, Func<T, string> selector)
    : Sampler<T>(yScaler, selector)
{
    private readonly float _margin = yScaler.Margin;
    private readonly float _height = yScaler.Height;

    public override List<(float, string)> Sample(IEnumerable<T> data, int count)
    {
        var list = data.ToList();
        var result = new List<(float, string)>();
        var step = (data.Count() - 1f) / (count - 1f);
        for (int i = 0; i < count; i++)
        {
            var index = (int)Math.Floor(i * step);
            var label = _selector(list[index]);
            var y = _margin + (count - 1 - i) * (_height - 2 * _margin) / (count - 1f);

            result.Add((y, label));
        }
        return result;
    }
}