using PerformanceApp.Server.Models;
using PerformanceApp.Server.Controllers;
using System.Xml.Linq;
using System.Globalization;

namespace PerformanceApp.Server.Services;

public class Plot
{
    private static readonly XNamespace svgNs = "http://www.w3.org/2000/svg";
    private static XElement GetSvgLine(decimal x1, decimal y1, decimal x2, decimal y2, string color = "black", int width = 1)
    {
        return new XElement(svgNs + "line",
            new XAttribute("x1", x1),
            new XAttribute("y1", y1),
            new XAttribute("x2", x2),
            new XAttribute("y2", y2),
            new XAttribute("stroke", color),
            new XAttribute("stroke-width", width)
        );
    }

    private static XElement GetSvgPolyline(List<string> points, string color, int width = 1)
    {
        return new XElement(svgNs + "polyline",
            new XAttribute("points", string.Join(" ", points)),
            new XAttribute("fill", "none"),
            new XAttribute("stroke", color),
            new XAttribute("stroke-width", width)
        );
    }

    private static string format(decimal x) => x.ToString("F2", CultureInfo.InvariantCulture);

    public static string CreateSVG(List<PortfolioBenchmarkCumulativeDayPerformanceDTO> ps)
    {
        const int width = 500;
        const int height = 300;
        const int border = 50;
        const int halfHeight = height / 2;

        var yMin = ps.Min(p => Math.Min(p.PortfolioValue, p.BenchmarkValue));
        var yMax = ps.Max(p => Math.Max(p.PortfolioValue, p.BenchmarkValue));

        decimal scaleX(decimal x) => border + x * (width - 2 * border)
            / ps.Count;
        decimal scaleY(decimal y) => halfHeight - y * (halfHeight - border)
            / Math.Max(Math.Abs(yMin), Math.Abs(yMax));

        List<decimal> xs = ps.Select((_, x) => scaleX(x)).ToList();
        List<decimal> y1s = ps.Select(p => scaleY(p.PortfolioValue)).ToList();
        List<decimal> y2s = ps.Select(p => scaleY(p.BenchmarkValue)).ToList();

        string toPoint(decimal x, decimal y) => $"{format(x)},{format(y)}";

        List<string> portfolioPoints = xs.Zip(y1s, toPoint).ToList();
        List<string> benchmarkPoints = xs.Zip(y2s, toPoint).ToList();

        var svg = new XElement(svgNs + "svg",
            new XAttribute("width", width),
            new XAttribute("height", height),
            GetSvgLine(0, scaleY(0), width, scaleY(0), "black", 1), // x axis
            GetSvgLine(scaleX(0), 0, scaleX(0), height, "black", 1), // y axis
            GetSvgPolyline(portfolioPoints, "blue", 1),
            GetSvgPolyline(benchmarkPoints, "red", 1)
        );

        int xTicks = 5;
        for (int i = 0; i < xTicks; i++)
        {
            int index = (int)Math.Round((decimal)i * (ps.Count - 1) / (xTicks - 1));
            string label = ps[index].Bankday.ToString();
            decimal x = border + i * (width - 2 * border) / (xTicks - 1);

            // Axis tick
            svg.Add(
                new XElement(svgNs + "line",
                    new XAttribute("x1", format(x)),
                    new XAttribute("y1", format(height / 2 - 5)),
                    new XAttribute("x2", format(x)),
                    new XAttribute("y2", format(height / 2 + 5)),
                    new XAttribute("stroke", "black")
                )
            );

            // Tick label
            svg.Add(
                new XElement(svgNs + "text",
                    new XAttribute("x", format(x)),
                    new XAttribute("y", format(height / 2 + 20)),
                    new XAttribute("font-size", "12"),
                    new XAttribute("text-anchor", "middle"),
                    label
                )
            );
        }

        int yTicks = 5;
        for (int i = 0; i <= yTicks; i++)
        {
            decimal yVal = yMin + i * (yMax - yMin) / yTicks;
            decimal yPos = scaleY(yVal);

            // Axis tick
            svg.Add(
                new XElement(svgNs + "line",
                    new XAttribute("x1", format(border - 5)),
                    new XAttribute("y1", format(yPos)),
                    new XAttribute("x2", format(border + 5)),
                    new XAttribute("y2", format(yPos)),
                    new XAttribute("stroke", "black")
                )
            );

            // Tick label
            svg.Add(
                new XElement(svgNs + "text",
                    new XAttribute("x", format(border - 7)),
                    new XAttribute("y", format(yPos + 4)),
                    new XAttribute("font-size", "12"),
                    new XAttribute("text-anchor", "end"),
                    yVal.ToString("P2", CultureInfo.InvariantCulture)
                )
            );
        }

        return svg.ToString();
    }

}

