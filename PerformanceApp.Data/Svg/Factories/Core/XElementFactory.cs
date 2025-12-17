using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Factories.Core;

public class XElementFactory
{
    private static readonly string SvgNamespace = "http://www.w3.org/2000/svg";
    public static XElement Create(string name, params XAttribute[] attributes)
    {
        return new XElement($"{SvgNamespace}{name}", attributes);
    }

    public static XElement Create(string name, IEnumerable<XAttribute> attributes)
    {
        return Create(name, attributes.ToArray());
    }

    public static XElement Create(string name, IEnumerable<(string, string)> attributes)
    {
        var xAttributes = XAttributeFactory.Create(attributes);

        return Create(name, xAttributes);
    }

    public static XElement Create(string name, string value, IEnumerable<XAttribute> attributes)
    {
        return new XElement($"{SvgNamespace}{name}", attributes, value);
    }

    public static XElement Create(string name, string value, IEnumerable<(string, string)> attributes)
    {
        var xAttributes = XAttributeFactory.Create(attributes);

        return Create(name, value, xAttributes);
    }
}
    