using System.Xml.Linq;
using PerformanceApp.Data.Svg.Defaults;

namespace PerformanceApp.Data.Svg.Builders;

public class XElementBuilder(string name)
{
    private readonly XElement _element = new(SvgDefaults.Namespace + name);

    public XElementBuilder WithAttribute<T>(string name, T value)
    {
        var s = value?.ToString()
            ?? throw new ArgumentException("Type must override ToString()");

        _element.Add(new XAttribute(name, s));
        return this;
    }

    public XElementBuilder WithElement(XElement element)
    {
        _element.Add(element);
        return this;
    }

    public XElementBuilder WithElements(IEnumerable<XElement> elements)
    {
        foreach (var element in elements)
        {
            _element.Add(element);
        }
        return this;
    }

    public XElementBuilder WithValue<T>(T value)
    {
        var s = value?.ToString()
            ?? throw new ArgumentException("Type must override ToString()");
        _element.Value = s;
        return this;
    }

    public XElement Build() => _element;
}