namespace PerformanceApp.Data.Svg.Enums;

public sealed class Color
{
    public string Value {get;}
    private Color(string value) => Value = value;
    public static Color Black => new("black");
    public static Color Line1 => new("#211f5e");
    public static Color Line2 => new("#ec646b");
}