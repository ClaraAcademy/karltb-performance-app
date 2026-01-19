using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Scalers;

public class XScaler(int width, int margin, int numberOfPoints)
    : LinearScaler(margin, width - 2 * margin, numberOfPoints - 1)
{ }