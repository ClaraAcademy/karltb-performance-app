namespace PerformanceApp.Data.Svg.Constants;

public static class LineConstants
{
    public static List<string> AttributeNames => [
        XAttributeConstants.X1,
        XAttributeConstants.Y1,
        XAttributeConstants.X2,
        XAttributeConstants.Y2,
        XAttributeConstants.Stroke,
        XAttributeConstants.StrokeWidth
    ];

    public static string ElementName => "line";

    public static string DefaultColor => "black";
    public static int DefaultWidth => 1;
}