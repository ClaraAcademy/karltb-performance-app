using System.Xml.Linq;

namespace PerformanceApp.Data.Svg.Builders;

public class XElementBuilder(string name)
{
    private readonly XElement _element = new(name);

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
    
    public XElementBuilder WithElement(XElementBuilder elementBuilder)
    {
        var element = elementBuilder.Build();
        _element.Add(element);
        return this;
    }

    public XElementBuilder WithValue<T>(T value)
    {
        var s = value?.ToString() 
            ?? throw new ArgumentException("Type must override ToString()");
        _element.Value = s;
        return this;
    }

    public XElement Build()
    {
        return _element;
    }
}