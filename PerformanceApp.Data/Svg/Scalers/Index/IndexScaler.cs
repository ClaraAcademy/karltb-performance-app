using PerformanceApp.Data.Svg.Scalers.Linear;

namespace PerformanceApp.Data.Svg.Scalers.Index;

public record IndexScaler(int Length, int Margin, int Total)
    : LinearScaler(Margin, (Length - 2f * Margin) / (Total - 1f))
{
}