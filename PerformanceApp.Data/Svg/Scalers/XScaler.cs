namespace PerformanceApp.Data.Svg.Scalers;

public class XScaler(int width, int margin, int numberOfPoints)
{
    private readonly int _width = width;
    private readonly int _margin = margin;
    private readonly int _numberOfPoints = numberOfPoints;

    public float Scale(float x)
    {
        return _margin + x * (_width - 2 * _margin) / _numberOfPoints;
    }
}