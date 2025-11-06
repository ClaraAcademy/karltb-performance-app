using PerformanceApp.Server.Models;
using PerformanceApp.Server.Controllers;
using System.Xml.Linq;
using System.Globalization;
using Microsoft.Data.SqlClient;
using System.Xml.Xsl;
using System.Runtime.CompilerServices;
using Microsoft.SqlServer.Server;
using System.Reflection.Emit;

namespace PerformanceApp.Server.DTOs;

public class SVG
{
    private readonly XNamespace SvgNamespace = "http://www.w3.org/2000/svg";
    private readonly int Width;
    private readonly int Height;
    private readonly int hMargin = 50;
    private readonly int vMargin = 50;
    private readonly int HalfHeight;
    private readonly float MinY;
    private readonly float MaxY;
    private readonly List<DataPoint2> DataPoints;
    private static readonly int TickHeight = 10;
    private static readonly int TickOffset = TickHeight / 2;
    private static readonly int LabelOffset = 10;

    private readonly XElement _schema;

    private float PairwiseMin(DataPoint2 d) => Math.Min(d.y1, d.y2);
    private float PairwiseMax(DataPoint2 d) => Math.Max(d.y1, d.y2);

    public SVG(List<DataPoint2> dataPoints, int width, int height)
    {
        Width = width;
        Height = height;
        HalfHeight = Height / 2;
        DataPoints = dataPoints;
        MinY = dataPoints.Min(PairwiseMin);
        MaxY = dataPoints.Max(PairwiseMax);
        _schema = GetSvg();
    }

    public override string ToString()
    {
        return _schema.ToString();
    }

    private XElement GetSvg()
    {
        List<float> xs = DataPoints.Select((_, x) => ScaleX(x)).ToList();
        List<float> y1s = DataPoints.Select(d => ScaleY(d.y1)).ToList();
        List<float> y2s = DataPoints.Select(d => ScaleY(d.y2)).ToList();

        List<string> portfolioPoints = xs.Zip(y1s, MapToPoint).ToList();
        List<string> benchmarkPoints = xs.Zip(y2s, MapToPoint).ToList();

        List<DateOnly> bankdays = DataPoints.Select(d => d.x).ToList();

        int xTicks = 5;
        int yTicks = 5;

        return new(SvgNamespace + "svg",
            new XAttribute("width", Width),
            new XAttribute("height", Height),
            GetXAxis(),
            GetYAxis(),
            GetPortfolioLine(portfolioPoints),
            GetBenchmarkLine(benchmarkPoints),
            GetXTicksAndLabels(xTicks),
            GetYTicksAndLabels(yTicks)
        );
    }


    private List<XElement> GetXTicksAndLabels(int numTicks)
    {
        List<XElement> result = [];
        float y = ScaleY(0f);
        for (int i = 0; i < numTicks; i++)
        {
            int index = (int)Math.Floor(i * (DataPoints.Count - 1f) / (numTicks - 1f));
            string label = DataPoints[index].x.ToString();
            float x = hMargin + i * (Width - 2f * hMargin) / (numTicks - 1f);

            // Skip origin tick
            if (i > 0)
            {
                result.Add(GetTick(x, y - TickOffset, x, y + TickOffset));
            }
            result.Add(GetLabel(x, y + LabelOffset, label, "start", 45));
        }
        return result;
    }

    private List<XElement> GetYTicksAndLabels(int numTicks)
    {
        List<XElement> result = [];
        for (int i = 0; i <= numTicks; i++)
        {
            float yVal = MinY + i * (MaxY - MinY) / numTicks;
            float yPos = ScaleY(yVal);

            // Skip origin tick
            if (i > 0)
            {
                result.Add(GetTick(vMargin - TickOffset, yPos, vMargin + TickOffset, yPos));
            }
            result.Add(GetLabel(vMargin - LabelOffset - 5, yPos + 0, FormatPercentage(yVal)));
        }
        return result;

    }

    private XElement GetTick(float x1, float y1, float x2, float y2)
        => new(SvgNamespace + "line",
            new XAttribute("x1", FormatDecimal(x1)),
            new XAttribute("y1", FormatDecimal(y1)),
            new XAttribute("x2", FormatDecimal(x2)),
            new XAttribute("y2", FormatDecimal(y2)),
            new XAttribute("stroke", "black")
        );

    private XElement GetLabel(float x, float y, string labelText, string anchor = "middle", int angle = 0)
        => new(SvgNamespace + "text",
            new XAttribute("x", FormatDecimal(x)),
            new XAttribute("y", FormatDecimal(y)),
            new XAttribute("font-size", "12"),
            new XAttribute("text-anchor", anchor),
            angle != 0 ? new XAttribute("transform", $"rotate({angle} {FormatDecimal(x)},{FormatDecimal(y)})") : null,
            labelText
        );

    private XElement GetXAxis() => GetSvgLine(0, ScaleY(0), Width, ScaleY(0), "black", 1);
    private XElement GetYAxis() => GetSvgLine(ScaleX(0), 0, ScaleX(0), Height, "black", 1);
    private XElement GetSvgLine(float x1, float y1, float x2, float y2, string color = "black", int width = 1)
        => new(SvgNamespace + "line",
            new XAttribute("x1", FormatDecimal(x1)),
            new XAttribute("y1", FormatDecimal(y1)),
            new XAttribute("x2", FormatDecimal(x2)),
            new XAttribute("y2", FormatDecimal(y2)),
            new XAttribute("stroke", color),
            new XAttribute("stroke-width", width)
        );

    private XElement GetPortfolioLine(List<string> portfolioPoints) => GetSvgPolyline(portfolioPoints, color: "blue", dotted: false);
    private XElement GetBenchmarkLine(List<string> benchmarkPoints) => GetSvgPolyline(benchmarkPoints, color: "red", dotted: true);
    private XElement GetSvgPolyline(List<string> points, string color, bool dotted = false)
        => new(SvgNamespace + "polyline",
            new XAttribute("points", string.Join(" ", points)),
            new XAttribute("fill", "none"),
            new XAttribute("stroke", color),
            new XAttribute("stroke-width", 1),
            dotted ? new XAttribute("stroke-dasharray", MapToPoint(1, 2)) : null
        );

    private float ScaleX(float x) => hMargin + x * (Width - 2 * hMargin) / DataPoints.Count;
    private float ScaleY(float y) => vMargin + (MaxY - y) * (Height - 2 * vMargin) / (MaxY - MinY);
    private static string FormatDecimal(float x) => x.ToString("F2", CultureInfo.InvariantCulture);
    private static string FormatPercentage(float x) => x.ToString("P0", CultureInfo.InvariantCulture);
    private string MapToPoint(float x, float y) => $"{FormatDecimal(x)},{FormatDecimal(y)}";
}
