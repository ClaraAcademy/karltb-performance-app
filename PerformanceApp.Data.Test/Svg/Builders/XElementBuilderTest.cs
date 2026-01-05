using System.Xml.Linq;
using PerformanceApp.Data.Svg.Builders;
using PerformanceApp.Data.Svg.Defaults;

namespace PerformanceApp.Data.Test.Svg.Builders;

public class XElementBuilderTest
{
    [Fact]
    public void Build_CreatesElementWithCorrectNameAndNamespace()
    {
        // Arrange
        var name = "SuperCoolUniqueName";
        var builder = new XElementBuilder(name);

        // Act
        var element = builder.Build();

        // Assert
        Assert.Equal(name, element.Name.LocalName);
        Assert.Equal(SvgDefaults.Namespace, element.Name.NamespaceName);
    }

    [Fact]
    public void WithAttribute_AddsAttributeToElement()
    {
        // Arrange
        var attributeName = "data-test-attribute";
        var attributeValue = "TestValue";

        var builder = new XElementBuilder("Dummy")
            .WithAttribute(attributeName, attributeValue);

        // Act
        var element = builder.Build();

        // Assert
        var attribute = element.Attribute(attributeName);
        Assert.NotNull(attribute);
        Assert.Equal(attributeValue, attribute?.Value);
    }

    [Fact]
    public void WithAttribute_AddsAttribute_FromIntegerValue()
    {
        // Arrange
        var attributeName = "data-integer-attribute";
        var attributeValue = 42;

        var builder = new XElementBuilder("Dummy")
            .WithAttribute(attributeName, attributeValue);

        // Act
        var element = builder.Build();

        // Assert
        var attribute = element.Attribute(attributeName);
        Assert.NotNull(attribute);
        Assert.Equal(attributeValue.ToString(), attribute?.Value);
    }

    [Fact]
    public void WithAttribute_ThrowsException_ForNullValue()
    {
        // Arrange
        var attributeName = "data-null-attribute";
        var builder = new XElementBuilder("Dummy");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.WithAttribute<string>(attributeName, null!));
    }

    [Fact]
    public void WithElement_AddsChildElement()
    {
        // Arrange
        var childName = "Child";
        var child = new XElement(childName);

        var builder = new XElementBuilder("Parent")
            .WithElement(child);

        // Act
        var element = builder.Build();

        // Assert
        var childElement = element.Element(childName);
        Assert.NotNull(childElement);
        Assert.Equal(childName, childElement?.Name.LocalName);
    }

    [Fact]
    public void WithElements_AddsMultipleChildElements()
    {
        // Arrange
        var child1 = new XElement("Child1");
        var child2 = new XElement("Child2");
        var children = new List<XElement> { child1, child2 };

        var builder = new XElementBuilder("Parent")
            .WithElements(children);

        // Act
        var element = builder.Build();

        // Assert
        Assert.NotNull(element.Element("Child1"));
        Assert.NotNull(element.Element("Child2"));
    }

    [Fact]
    public void WithElements_DoesNotAddEmptyCollection()
    {
        // Arrange
        var children = new List<XElement>();

        var builder = new XElementBuilder("Parent")
            .WithElements(children);

        // Act
        var element = builder.Build();

        // Assert
        Assert.Empty(element.Elements());
    }

    [Fact]
    public void WithValue_SetsElementValue()
    {
        // Arrange
        var value = "This is a test value";

        var builder = new XElementBuilder("Dummy")
            .WithValue(value);

        // Act
        var element = builder.Build();

        // Assert
        Assert.Equal(value, element.Value);
    }

    [Fact]
    public void WithValue_SetsElementValue_FromInteger()
    {
        // Arrange
        var value = 12345;

        var builder = new XElementBuilder("Dummy")
            .WithValue(value);

        // Act
        var element = builder.Build();

        // Assert
        Assert.Equal(value.ToString(), element.Value);
    }

    [Fact]
    public void WithValue_ThrowsException_ForNullValue()
    {
        // Arrange
        var builder = new XElementBuilder("Dummy");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => builder.WithValue<string>(null!));
    }

}