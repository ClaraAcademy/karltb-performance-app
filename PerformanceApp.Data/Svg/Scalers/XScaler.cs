using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Scalers;

public class XScaler(int width, int margin, int numberOfPoints)
    : IScaler
{
    private readonly int _width = width;
    private readonly int _margin = margin;
    private readonly int _numberOfPoints = numberOfPoints;

    public int Width => _width;
    public int Margin => _margin;
    public int NumberOfPoints => _numberOfPoints;

    private float StepSize => (Width - 2f * Margin) / NumberOfPoints;
    public float Scale(float x) => Margin + x * StepSize;
    public float Scale(int x) => Scale((float)x);
}