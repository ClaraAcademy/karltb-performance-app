using PerformanceApp.Data.Svg.Scalers.Interface;

namespace PerformanceApp.Data.Svg.Common;

public record ScalerPair(IScaler X, IScaler Y);