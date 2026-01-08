using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Factories.Core.Interfaces;

public interface ILineFactory
{
    XElement Create(float x1, float y1, float x2, float y2);
}