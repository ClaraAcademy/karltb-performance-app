using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Samplers.Interface;

public interface ISampler
{
    public IEnumerable<XElement> Ticks { get; }
    public IEnumerable<XElement> Labels { get; }
}