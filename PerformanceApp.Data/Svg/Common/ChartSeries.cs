namespace PerformanceApp.Data.Svg.Common;

public class ChartSeries(IEnumerable<float> values, string color)
{
    public List<float> Values { get; } = values.ToList();

    public string Color { get; } = color;
    public int Count => Values.Count;
    public float Max => Values.Max();
    public float Min => Values.Min();
    public static ChartSeries From<T>(IEnumerable<T> values, Func<T, float> toFloat, string color)
    {
        return new ChartSeries(values.Select(toFloat), color);
    }
}