using System.Xml.Linq;
using PerformanceApp.Data.Svg.Constants;

namespace PerformanceApp.Data.Svg.Builders;

public class AxisBuilder
{
    private float _x1;
    private float _y1;
    private float _x2;
    private float _y2;

    public AxisBuilder From(float x1, float y1)
    {
        _x1 = x1;
        _y1 = y1;
        return this;
    }
    public AxisBuilder To(float x2, float y2)
    {
        _x2 = x2;
        _y2 = y2;
        return this;
    }
    public XElement Build()
    {
        return new LineBuilder()
            .WithColor(ColorConstants.Black)
            .WithWidth(1)
            .From(_x1, _y1)
            .To(_x2, _y2)
            .Build();
    }
}
