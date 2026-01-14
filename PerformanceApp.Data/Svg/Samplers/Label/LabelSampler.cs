using PerformanceApp.Data.Svg.Samplers.Label.Index;

namespace PerformanceApp.Data.Svg.Samplers.Label;

public class LabelSampler<T>(IndexSampler indexSampler, IEnumerable<T> data, Func<T, string> labelSelector)
{
    private readonly IndexSampler _indexSampler = indexSampler;
    private readonly Func<T, string> _labelSelector = labelSelector;
    private readonly IEnumerable<T> _data = data;

    public LabelSampler(IEnumerable<T> data, Func<T, string> labelSelector, int samples)
        : this(new IndexSampler(data.Count(), samples), data, labelSelector)
    { }

    public IReadOnlyList<string> Sample()
    {
        return _indexSampler
            .Sample()
            .Select(_data.ElementAt)
            .Select(_labelSelector)
            .ToList();
    }
}