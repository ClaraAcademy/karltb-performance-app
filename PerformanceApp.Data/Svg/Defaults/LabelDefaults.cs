using System.Drawing;
using PerformanceApp.Data.Svg.Enums;

namespace PerformanceApp.Data.Svg.Defaults;

public record Anchors(Anchor X, Anchor Y);
public class LabelDefaults
{
    public static Point Offset => new(15, -10);
    public static PointF Angle => new(45f, 0f);
    public static Anchors Anchor => new(Enums.Anchor.Start, Enums.Anchor.End);
    public const int Size = 12;
}