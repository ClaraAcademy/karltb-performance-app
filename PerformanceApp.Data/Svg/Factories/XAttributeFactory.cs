using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Factories;

public class XAttributeFactory
{
    public static IEnumerable<XAttribute> Create(IEnumerable<(string name, string value)> attributes)
    {
        return attributes.Select(Create);
    }
    public static XAttribute Create((string name, string value) attribute)
    {
        return Create(attribute.name, attribute.value);
    }

    public static XAttribute Create(string name, string value)
    {
        return new XAttribute(name, value);
    }

    public static XAttribute Create<T>(string name, T value)
        where T : IFormattable
    {
        var s = value.ToString() 
            ?? throw new ArgumentException("Type must override ToString()");

        return Create(name, s);
    }
}