namespace PerformanceApp.Data.Svg.Enums;

public sealed class Anchor
{
    public string Value { get; }
    private Anchor(string value) => Value = value;
    public static Anchor Start => new("start");
    public static Anchor Middle => new("middle");
    public static Anchor End => new("end");
}