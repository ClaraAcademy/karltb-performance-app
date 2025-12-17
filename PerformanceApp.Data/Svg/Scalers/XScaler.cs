namespace PerformanceApp.Data.Svg.Scalers;

public class XScaler
{
    private readonly float _width;
    private readonly float _margin;
    private readonly int _numberOfPoints;

    public float Scale(float x)
    {
        return _margin + x * (_width - 2 * _margin) / _numberOfPoints;
    }
}