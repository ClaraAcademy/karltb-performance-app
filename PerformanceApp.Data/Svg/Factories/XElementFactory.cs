using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Factories;

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
        var xAttributes = attributes
            .Select(XAttributeFactory.Create);

        return Create(name, xAttributes);
    }
}