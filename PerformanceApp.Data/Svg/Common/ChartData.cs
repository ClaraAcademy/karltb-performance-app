namespace PerformanceApp.Data.Svg.Common;

public class ChartData(List<string> xs, List<ChartSeries> series)
{
    private readonly List<string> _xs = xs;
    public List<ChartSeries> Series { get; } = series;

    public string GetXLabel(int index) => _xs[index]?.ToString() ?? string.Empty;

    public int PointCount => Series.Max(s => s.Count);
    public float Max => Series.Max(s => s.Max);
    public float Min => Series.Min(s => s.Min);
}