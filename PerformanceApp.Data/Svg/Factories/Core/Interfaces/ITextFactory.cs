using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Factories.Core.Interfaces;

public interface ITextFactory
{
    XElement Create(string text, float x, float y, string anchor = "middle", float angle = 0);
}