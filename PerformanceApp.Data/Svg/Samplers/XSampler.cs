using PerformanceApp.Data.Svg.Samplers.Base;
using PerformanceApp.Data.Svg.Scalers;

namespace PerformanceApp.Data.Svg.Samplers;

public class XSampler<T>(XScaler xScaler, Func<T, string> selector)
    : Sampler<T>(xScaler, selector)
{
    private readonly float _margin = xScaler.Margin;
    private readonly float _doubleMargin = 2 * xScaler.Margin;
    private readonly float _width = xScaler.Width;

    public override List<(float, string)> Sample(IEnumerable<T> data, int count)
    {
        var list = data.ToList();
        var result = new List<(float, string)>();
        var step = (data.Count() - 1f) / (count - 1f);
        for (int i = 0; i < count; i++)
        {
            var index = (int)Math.Floor(i * step);
            var label = _selector(list[index]);
            var x = _margin + i * (_width - _doubleMargin) / (count - 1f);

            result.Add((x, label));
        }
        return result;
    }
}