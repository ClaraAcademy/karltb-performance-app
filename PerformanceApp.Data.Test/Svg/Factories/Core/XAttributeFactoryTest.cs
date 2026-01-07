using PerformanceApp.Data.Svg.Factories.Core;

namespace PerformanceApp.Data.Test.Svg.Factories.Core;

public class XAttributeFactoryTest
{
    static string CreateAttribute(int i = 0) => $"test-attribute-{i}";
    static string CreateValue(int i = 0) => $"test-value-{i}";

    [Fact]
    public void Create_CreatesAttribute()
    {
        // Arrange
        var name = CreateAttribute();
        var value = CreateValue();

        // Act
        var attribute = XAttributeFactory.Create(name, value);

        // Assert
        Assert.Equal(name, attribute.Name.LocalName);
        Assert.Equal(value, attribute.Value);
    }

    [Fact]
    public void Create_CreatesAttribute_FromTuple()
    {
        // Arrange
        var name = CreateAttribute();
        var value = CreateValue();
        var tuple = (name, value);

        // Act
        var attribute = XAttributeFactory.Create(tuple);

        // Assert
        Assert.Equal(name, attribute.Name.LocalName);
        Assert.Equal(value, attribute.Value);
    }

    [Fact]
    public void Create_CreatesAttributes_FromEnumerable()
    {
        // Arrange
        var n = 10;
        var expected = Enumerable.Range(1, n)
            .Select(i => (CreateAttribute(i), CreateValue(i)))
            .ToList();

        // Act
        var attributes = XAttributeFactory
            .Create(expected)
            .ToList();

        // Assert
        foreach (var (e, a) in expected.Zip(attributes, (e, a) => (e, a)))
        {
            var eName = e.Item1;
            var eValue = e.Item2;

            var aName = a.Name.LocalName;
            var aValue = a.Value;

            Assert.Equal(eName, aName);
            Assert.Equal(eValue, aValue);
        }
    }
}