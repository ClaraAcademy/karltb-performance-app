namespace PerformanceApp.Data.Svg.Samplers.Coordinate;

public class CoordinateFactory<T>(IEnumerable<T> values, Func<T, float> scale)
{
    public IEnumerable<float> Coordinates => values.Select(scale);
}
